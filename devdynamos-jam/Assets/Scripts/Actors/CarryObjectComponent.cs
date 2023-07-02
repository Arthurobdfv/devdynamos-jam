using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObjectComponent : MonoBehaviour
{
    /// <summary>
    /// Velocidade atual de movimento do objeto, e alterada caso carregando algum objeto
    /// </summary>
    public float CurrentSpeed => IsCarrying ? _speed * _carryingSlowRate : _speed;
    /// <summary>
    /// Propriedade para expor a PickupDistance
    /// </summary>
    public float PickupDistance => _pickupDistance;

    /// <summary>
    /// PickupDistance ao quadrado (performance)
    /// </summary>
    public float SquaredPickupDistance => _pickupDistance * _pickupDistance;

    /// <summary>
    /// Velocidade do objeto;
    /// </summary>
    [SerializeField] private float _speed;
    /// <summary>
    /// Taxa com que o objeto reduz a velocidade ao carregar algo
    /// </summary>
    [SerializeField] private float _carryingSlowRate;
    /// <summary>
    /// Distancia necessaria para carregar outro objeto
    /// </summary>
    [SerializeField] private float _pickupDistance;

    /// <summary>
    /// Objeto que esse behaviour esta carrregando
    /// </summary>
    private ICarregavel _carriedObject = null;

    /// <summary>
    /// Indica se o objeto esta carregando algo
    /// </summary>
    public bool IsCarrying =>  _carriedObject != null;

    /// <summary>
    /// Metodo usado para pegar um objeto
    /// </summary>
    /// <param name="obj"></param>
    public void PickUp(ICarregavel obj)
    {
        if (!IsCarrying && IsInPickupRange(obj.CurrentPosition))
        {
            _carriedObject = obj;
            obj.OnPickUp(gameObject);
        }
    }

    /// <summary>
    /// Metodo usado para dropar o objeto que esta carregando
    /// </summary>
    public void Drop(bool move = false)
    {
        if (IsCarrying)
        {
            _carriedObject.OnDrop(move);
            _carriedObject = null;
        }
    }

    bool IsInPickupRange(Transform obj)
    {
        return (obj.position - transform.position).sqrMagnitude < (_pickupDistance * _pickupDistance);
    }
}
