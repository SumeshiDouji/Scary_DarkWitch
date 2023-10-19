using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    BeamType beamType;
    Vector3 beamSpeed;
    public enum BeamType
    {
        UeBeam = 0,
        ShitaBeam = 1
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (beamType)
        {
            case BeamType.UeBeam:
                transform.position += beamSpeed * Time.deltaTime;
                break;
            case BeamType.ShitaBeam:
                break;
        }
    }
    public void Beam_Set(BeamType beam_Type,Vector3 beam_Speed)
    {
        beamType = beam_Type;
        beamSpeed = beam_Speed;
    }
}
