using UnityEngine;

public class ObjetoCarregavel : MonoBehaviour, ICarregavel
{
    /// <summary>
    /// Expoe propriedade que diz se este objeto esta sendo carregado
    /// </summary>
    public bool BeingCarried => _carryingObject != null;

    /// <summary>
    /// Expoe o Transform do objeto
    /// </summary>
    Transform ICarregavel.CurrentPosition => transform;

    /// <summary>
    /// Referencia ao objeto que esta carregando este objeto
    /// </summary>
    public GameObject _carryingObject;
    
    // Metodo para dropar esse item
    public void OnDrop()
    {
        if (!BeingCarried) return;
        _carryingObject = null;
        DropVisualUpdate();
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
    }

    // Metodo usao para fazer as alteracoes visuais quando o objeto e pego
    private void DropVisualUpdate()
    {
        // Reativar o sprite renderer ou fazer alteracoes visuais ao ser largado no chao;
        // Desativar icone indicando quem esta carregando este item
    }
}
