using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField]
    private GameObject _jogadorParaSeguir;
    [SerializeField]
    private float _rangeDeVisao;
    [SerializeField]
    private float _distanciaMaxDoPlayer;

    private float _distanciaMaxDoPlayerAoQuadrado => _distanciaMaxDoPlayer * _distanciaMaxDoPlayer;

    [SerializeField]
    private bool _estaCaminhando;
    [SerializeField]
    private bool _estaPatroling;
    [SerializeField]
    private ICarregavel _galaoDeGasolina;
    private bool _carregandoObjeto;
    [SerializeField]
    private float _robotMoveSpeed;

    IEnumerable _patrolCoroutine;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DistanciaDoPlayerAoQuadrado() > _distanciaMaxDoPlayerAoQuadrado)
            MoverParaPlayer();
        else if(!_estaPatroling) StartCoroutine(Patrol());
    }

    private void ChecarSeGalaoEstaPerto()
    {
        if (_galaoDeGasolina != null)
            return;
        var collisions = Physics2D.OverlapCircleAll(transform.position, _rangeDeVisao, (int)LayerMaskEnum.Gasolina);
        var objetoCarregavel = collisions.FirstOrDefault(x => x.GetComponent<ICarregavel>() != null).GetComponent<ICarregavel>();
        if(objetoCarregavel != null)
            _galaoDeGasolina = objetoCarregavel;
    }

    private void MoverParaPlayer()
    {
        StopAllCoroutines();
        _estaPatroling = false;
        var directionToWalk = (_jogadorParaSeguir.transform.position - transform.position).normalized;
        transform.Translate(directionToWalk * Time.deltaTime * _robotMoveSpeed);
    }

    private IEnumerator Patrol()
    {
        _estaPatroling = true;
        Vector2 patrolPoint = (Random.insideUnitCircle * Random.Range(0, _distanciaMaxDoPlayer)) + (Vector2)_jogadorParaSeguir.transform.position;
        var directionToWalk = (patrolPoint - (Vector2)transform.position);
        while (directionToWalk.sqrMagnitude > .5f)
        {
            transform.Translate(directionToWalk.normalized * _robotMoveSpeed * Time.deltaTime);
            directionToWalk = (patrolPoint - (Vector2)transform.position);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(4);
        _estaPatroling = false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _rangeDeVisao);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _distanciaMaxDoPlayer);
    }

    private float DistanciaDoPlayerAoQuadrado()
    {
        return (_jogadorParaSeguir.transform.position - transform.position).sqrMagnitude;
    }
}
