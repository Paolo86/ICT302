﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentPositionTaskSlot : MonoBehaviour
{
    public Instrument.INSTRUMENT_TAG CurrentInstrument;
    public Instrument.INSTRUMENT_TAG CorrectInstrument;

    public void OnPointing()
    {
        if(CurrentInstrument == Instrument.INSTRUMENT_TAG.NONE)
        {
            foreach (Material mat in GetComponentInChildren<Renderer>().materials)
            {
                mat.color = new Color(0, 1, 0, 0.5f);
            }
        }
    }

    public void OnReleasedPointing()
    {
        foreach (Material mat in GetComponentInChildren<Renderer>().materials)
        {
            mat.color = new Color(1,1,1,0.5f);
        }
    }

    private void Start()
    {
        foreach (Material mat in GetComponentInChildren<Renderer>().materials)
        {
            mat.color = new Color(1, 1, 1, 0.5f);
        }
    }
}
