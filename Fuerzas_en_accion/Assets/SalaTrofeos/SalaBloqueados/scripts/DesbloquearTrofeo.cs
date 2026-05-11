using UnityEngine;

public class DesbloquearTrofeo : MonoBehaviour
{
    public enum TipoTrofeo
    {
        Bronce,
        Plata,
        Oro
    }

    [Header("Trofeo a desbloquear")]
    public TipoTrofeo tipoTrofeo;

    private string ObtenerClave()
    {
        switch (tipoTrofeo)
        {
            case TipoTrofeo.Bronce:
                return "TrofeoBronce";
            case TipoTrofeo.Plata:
                return "TrofeoPlata";
            case TipoTrofeo.Oro:
                return "TrofeoOro";
        }

        return "";
    }

    // Llama a este método cuando completes la animación del candado
    public void Desbloquear()
    {
        string clave = ObtenerClave();

        PlayerPrefs.SetInt(clave, 1);
        PlayerPrefs.Save();

        Debug.Log("Trofeo desbloqueado: " + clave);
    }

    // Método opcional para reiniciar solo este trofeo
    public void Reiniciar()
    {
        PlayerPrefs.DeleteKey(ObtenerClave());
        PlayerPrefs.Save();
    }
}