using System.Collections;
using UnityEngine;

public class ObjetoCarregavel : MonoBehaviour, ICarregavel
{
    /// <summary>
    /// Expoe propriedade que diz se este objeto esta sendo carregado
    /// </summary>
    public bool BeingCarried => _carryingObject != null;

    [SerializeField] private float _onDropMovementSpeed = 0.5f;
    [SerializeField] private float _onDropAnimationSpeed = 0.3f;

    /// <summary>
    /// Expoe o Transform do objeto
    /// </summary>
    Transform ICarregavel.CurrentPosition => transform;

    public GameObject CarryingIndicator;
    public GameObject ObjectSprite;

    /// <summary>
    /// Referencia ao objeto que esta carregando este objeto
    /// </summary>
    public GameObject _carryingObject;
    
    // Metodo para dropar esse item
    public void OnDrop(bool move = false)
    {
        if (!BeingCarried) return;
        _carryingObject = null;
        DropVisualUpdate();
        if (move)
        {
            StartCoroutine(DropMovement());
        }
    }

    // Metodo para pegar esse item
    public void OnPickUp(GameObject obj)
    {
        if (BeingCarried) return;
        _carryingObject = obj;
        PickupVisualUpdate();
    }

    public void FixedUpdate()
    {
        if (BeingCarried) transform.position = _carryingObject.transform.position;
    }

    // Matodo usado para fazer as alteracoes visuais quando o objeto e pego
    private void PickupVisualUpdate()
    {
        // Desativar o sprite renderer ou fazer alteracoes visuais ao ser carregado;
        // Podemos desativar o sprite renderer e ativar um Icone do item em cima do objeto que esta carregando
        CarryingIndicator.SetActive(true);
        ObjectSprite.SetActive(false);
    }

    // Metodo usao para fazer as alteracoes visuais quando o objeto e pego
    private void DropVisualUpdate()
    {
        // Reativar o sprite renderer ou fazer alteracoes visuais ao ser largado no chao;
        // Desativar icone indicando quem esta carregando este item
        CarryingIndicator.SetActive(false);
        ObjectSprite.SetActive(true);
    }

    private IEnumerator DropMovement()
    {
        var movementDirection = (Vector2)Random.insideUnitCircle * 2 + (Vector2)transform.position;
        var startPos = transform.position;
        var animTime = 0f;
        while(animTime < _onDropAnimationSpeed)
        {
            yield return new WaitForFixedUpdate();
            transform.position = Vector3.Lerp(startPos, movementDirection, animTime);
            animTime += Time.deltaTime;
        }
    }
}
