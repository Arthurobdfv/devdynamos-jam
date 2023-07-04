using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CarryObjectComponent))]
public class RobotBehaviour : MonoBehaviour
{
    public float CurrentOverLoad => _currentOverload;
    public float CurrentOverloadRate => _currentOverload / _maxOverloadBar;
    private bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            if (value != _isMoving)
            {
                _isMoving = value;
                OnMoveChange(value);
            }
        }
    }
    /// <summary>
    /// Variavel que define o player no quao o robo vai seguir
    /// </summary>
    [SerializeField] private GameObject _playerToFollow;
    /// <summary>
    /// O Transform component da nave na qual o robo vai levar o galao
    /// </summary>
    [SerializeField] private Transform _spaceshipPosition;
    /// <summary>
    /// O galao de gasolina que o robo encontrou
    /// </summary>
    [SerializeField] private ICarregavel? _galaoDeGasolina;
    /// <summary>
    /// O componente que auxilia as interacoes com objetos carregaveis
    /// </summary>
    [SerializeField] private CarryObjectComponent _carryObjectComponent;
    private CircleCollider2D _circleCollider;

    /// <summary>
    /// A distancia que o robo enxerga o galao
    /// </summary>
    [SerializeField] private float _visionRange;

    /// <summary>
    /// Distancia maxima que o robo pode ficar do player que esta seguindo sem se mover.
    /// </summary>
    [SerializeField] private float _playerMaxDistance;

    /// <summary>
    /// A move speed do robo, referenciando o componente de carregar objetos pois a movespeed e alterada enquanto carregando
    /// </summary>
    [SerializeField] private float _robotMoveSpeed => _carryObjectComponent.CurrentSpeed;
    /// <summary>
    /// Tempo que o robo leva para escanear a area de visao procurando por galoes
    /// </summary>
    [SerializeField] private float _scanTime;
    [SerializeField] private float _overChargeEnemyDamage;
    [SerializeField] private AudioClip _robotFoundSomething;
    [SerializeField] private AudioClip _robotLostEnergy;
    [SerializeField] private AudioClip _robotRefillEnergy;

    [SerializeField] private Animator _robotAnimator;
    [SerializeField] private bool _isCollecting;
    [SerializeField] private GameObject _robotSprite;

    #region Variaveis de controle de estado
    /// <summary>
    /// Indica se o robo esta se movendo
    /// </summary>
    [SerializeField] private bool _isMoving;
    /// <summary>
    /// Indica se o robo esta escaneando por objetos
    /// </summary>
    [SerializeField] private bool _isScanning;
    /// <summary>
    /// Indica se o robo esta fazendo patrol em volta do player
    /// </summary>
    [SerializeField] private bool _isPatroling;
    /// <summary>
    /// indica se o robo ja escaneou a area em que ele esta
    /// </summary>
    [SerializeField] private bool _scannedPosition;
    /// <summary>
    /// Indica se ele encontrou um objeto para pegar
    /// </summary>
    [SerializeField] private bool _foundFuel => _galaoDeGasolina != null;
    /// <summary>
    /// Indica se o robo esta sobrecarregado
    /// </summary>
    [SerializeField] private bool _isOverloaded;
    /// <summary>
    /// Velocidade com a qual o overload enche
    /// </summary>
    [SerializeField] private float _overloadRate;
    /// <summary>
    /// Velocidade com a qual recarrega a barra
    /// </summary>
    [SerializeField] private float _overloadRechargeRate;
    /// <summary>
    /// Limite de overload do robo antes de explodir
    /// </summary>
    [SerializeField] private float _maxOverloadBar;
    [SerializeField] private float _currentOverload;
    [SerializeField] private float _overloadTime;
    #endregion

    IEnumerable _patrolCoroutine;

    // Start is called before the first frame update
    void Awake()
    {
        _carryObjectComponent = GetComponent<CarryObjectComponent>() ?? throw new MissingComponentException(nameof(CarryObjectComponent));
        _circleCollider = GetComponent<CircleCollider2D>() ?? throw new MissingComponentException(nameof(CarryObjectComponent));
        _circleCollider.radius = _visionRange;
        _currentOverload = _maxOverloadBar;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!SceneManage.Instance.GameStarted) return;
        StateControl();
        _robotAnimator.SetBool("IsWalking", _isMoving);
        _robotAnimator.SetBool("IsScanning", _isScanning);
        _robotAnimator.SetBool("Collecting", _carryObjectComponent.IsCarrying);
    }

    /// <summary>
    /// Basicamente o cerebro do robo, controla os varios states dele.
    /// </summary>
    void StateControl()
    {
        CalculateOverLoadChance();
        if (_isOverloaded)
            return;
        else if (!_carryObjectComponent.IsCarrying && _galaoDeGasolina != null)
        {
            MoveTowards(_galaoDeGasolina.CurrentPosition);
            TryPickupFuel();
        }
        else if (_carryObjectComponent.IsCarrying)
        {
            MoveTowards(_spaceshipPosition);
            TryDropFuelOnSpaceShip();
        }
        else if (SquareDistanceFromPlayer() > SquareFloat(_playerMaxDistance) && !_isScanning)
            MoveTowards(_playerToFollow.transform);
        else
        {
            IsMoving = false;
            if (!IsMoving && !_scannedPosition && !_isScanning) StartCoroutine(Scan());
            if (!_isPatroling && !IsMoving && !_isScanning) StartCoroutine(Patrol());
        }
    }

    private void CalculateOverLoadChance()
    {
        var carryingItem = _carryObjectComponent.IsCarrying;
        if (_currentOverload <= 0)
        {
            StopAllCoroutines();
            if (carryingItem) _carryObjectComponent.Drop(true);
            carryingItem = false;
            _galaoDeGasolina = null;
            AudioManager.PlaySound(_robotLostEnergy);
            StartCoroutine(Overload());
        }
        if (carryingItem)
            _currentOverload -= _overloadRate * Time.deltaTime;
        else _currentOverload += (_overloadRate / 2) * Time.deltaTime;
        _currentOverload = Mathf.Clamp(_currentOverload, 0, _maxOverloadBar);
    }

    private void TryDropFuelOnSpaceShip()
    {
        if (SquareDistanceFrom(_spaceshipPosition) <= _carryObjectComponent.SquaredPickupDistance)
        {
            _galaoDeGasolina = null;
            _carryObjectComponent.Drop();
            // Aqui podemos adicionar referencia aos scripts de vitoria do jogo e / ou scripts da nave ao receber a gasolina
        }
    }

    // Metodo que checa se o galao esta no campo de visao do Robo
    private void CheckFuelNearby()
    {
        if (_galaoDeGasolina != null)
            return;
        var collisions = new List<Collider2D>();
        var numberOfCollitions = _circleCollider.OverlapCollider(new ContactFilter2D() { layerMask = (int)LayerMaskEnum.Gasolina }, collisions);
        var objetoCarregavel = collisions.FirstOrDefault(x => x.GetComponent<ICarregavel>() != null)?.gameObject;
        if (objetoCarregavel != null)
        {
            _robotAnimator.SetTrigger("Found");
            AudioManager.PlaySound(_robotFoundSomething);
            _galaoDeGasolina = objetoCarregavel.GetComponent<ICarregavel>();
        }
    }

    // Metodo pra tentar pegar o fuel
    private void TryPickupFuel()
    {
        _carryObjectComponent.PickUp(_galaoDeGasolina);
        _robotAnimator.Play("Robo_collect");
    }

    // Metodo principal de movimentacao do robo
    private void MoveTowards(Transform targetPosition)
    {
        StopAllCoroutines();
        _isPatroling = false;
        IsMoving = true;
        var directionToWalk = (targetPosition.position - Position).normalized;
        _robotAnimator.SetFloat("Horizontal", directionToWalk.x);
        _robotAnimator.SetFloat("Vertical", directionToWalk.y);
        // Gambiarra braba
        _robotSprite.transform.localScale = new Vector2(Mathf.Abs(_robotSprite.transform.localScale.x) * (directionToWalk.x > 0 ? 1 : -1), _robotSprite.transform.localScale.y);
        transform.Translate(directionToWalk * Time.deltaTime * _robotMoveSpeed);
    }

    // Metodo onde o robo esta overloaded
    private IEnumerator Overload()
    {
        _isOverloaded = true;
        var rechargeRate = (float)_maxOverloadBar / _overloadTime;
        float time = 0f;
        while (time < _overloadTime)
        {
            yield return new WaitForFixedUpdate();
            _currentOverload += rechargeRate * Time.deltaTime;
            time += Time.deltaTime;
        }
        _isOverloaded = false;
        _currentOverload = _maxOverloadBar;
        AudioManager.PlaySound(_robotRefillEnergy);
        StartCoroutine(Scan());
    }


    private IEnumerator Scan()
    {
        Debug.Log("Starting Scan");
        
        _isScanning = true;
        float timeSinceStart = 0f;
        while (timeSinceStart < _scanTime)
        {
            yield return new WaitForFixedUpdate();
            timeSinceStart += Time.deltaTime;
        }
        CheckFuelNearby();
        Debug.Log("Finished Scan");
        _isScanning = false;
        _scannedPosition = true;
    }

    // Corotina de patrol do Robo, pode ser interrompida quando ele esta seguindo algo
    private IEnumerator Patrol()
    {
        _isPatroling = true;
        IsMoving = true;
        yield return new WaitForSeconds(3);
        Vector2 patrolPoint = (Random.insideUnitCircle * Random.Range(0, _playerMaxDistance)) + (Vector2)_playerToFollow.transform.position;
        var directionToWalk = (patrolPoint - (Vector2)Position);
        while (directionToWalk.sqrMagnitude > .5f)
        {
            transform.Translate(directionToWalk.normalized * _robotMoveSpeed * Time.deltaTime);
            directionToWalk = (patrolPoint - (Vector2)Position);
            yield return new WaitForFixedUpdate();
        }
        IsMoving = false;
        _isPatroling = false;
    }

    // Metodo ativado quando o IsMoving altera de valor.
    private void OnMoveChange(bool newValue)
    {
        if (newValue == false)
            _scannedPosition = false;
        else
        {
            _isScanning = false;
        }

    }

    public void OnHit(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            if (!_isOverloaded) _currentOverload = Mathf.Clamp(_currentOverload - _overChargeEnemyDamage, 0f, _maxOverloadBar);
            Destroy(collision.gameObject);
        }
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Position, _visionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Position, _playerMaxDistance);
    }
    #endregion

    // Metodos utilitarios para o codigo ficar mais legivel e facil de entender.
    #region Utility
    private Vector3 Position => transform.position;
    private float SquareDistanceFromPlayer()
    {
        return SquareDistanceFrom(_playerToFollow.transform);
    }

    private float SquareDistanceFrom(Transform t)
    {
        return (t.transform.position - transform.position).sqrMagnitude;
    }
    private float SquareFloat(float f) => f * f;
    #endregion
}

