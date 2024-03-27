using UnityEngine;

public class billboard : MonoBehaviour
{
    public Camera m_MainCamera;
    Quaternion m_OriginalRotation;

    void Start()
    {
        if(m_MainCamera == null)
            m_MainCamera = Camera.main;
        m_OriginalRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation
            = m_MainCamera.transform.rotation * m_OriginalRotation;
    }
}