using UnityEngine;

/// <summary>
/// Interface que deve ser herdada para objetos carregavies (Caso tenhamos mais de um)
/// </summary>
public interface ICarregavel
{
    public bool BeingCarried { get; }
    public Transform CurrentPosition { get; }
    public void OnPickUp(GameObject obj);
    public void OnDrop();
}
