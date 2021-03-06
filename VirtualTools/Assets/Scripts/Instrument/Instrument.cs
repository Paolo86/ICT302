﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour, IInstrumentSelectable
{
    public Vector3 originalPosition;
    public Quaternion originalRotation;

    private Renderer m_renderer;

    /// <summary>
    /// List of all instrument in enum form. Instruments are identified by enum values
    /// </summary>
    public enum INSTRUMENT_TAG
    {
        ADDSON_BROWN_FORCEPS,
        SCALPEL,
        METZEMBAUM_SCISSOR,
        ROCHESTER_CARMALT_FORCEPS,
        MAYO_HEGAR_NEEDLE_DRIVER,
        SUTURE_SCISSOR,
        MAYO_SCISSOR,
        TOWEL_CLAMPS,
        NONE
    }

    public INSTRUMENT_TAG instrumentTag;
    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponentInChildren<MeshRenderer>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void OnPointing()
    {
        foreach (Material mat in GetComponentInChildren<Renderer>().materials)
        {
            mat.color = Color.green;
        }
    }

    public void OnReleasedPointing()
    {
        foreach (Material mat in GetComponentInChildren<Renderer>().materials)
        {
            mat.color = Color.white;
        }
    }

    /// <summary>
    /// Get instrument name as string
    /// </summary>
    /// <param name="tag">Instrument enum</param>
    /// <returns>The name of the instrument as string</returns>
    public static string GetName(INSTRUMENT_TAG tag)
    {
        switch (tag)
        {
            case INSTRUMENT_TAG.ADDSON_BROWN_FORCEPS:
                return "Addson-Brown Forceps";
            case INSTRUMENT_TAG.MAYO_HEGAR_NEEDLE_DRIVER:
                return "Mayo Hegar Needle Driver";
            case INSTRUMENT_TAG.MAYO_SCISSOR:
                return "Mayo Scissor";
            case INSTRUMENT_TAG.METZEMBAUM_SCISSOR:
                return "Metzembaum Scissor";
            case INSTRUMENT_TAG.ROCHESTER_CARMALT_FORCEPS:
                return "Rochester Carmalt Forceps";
            case INSTRUMENT_TAG.SCALPEL:
                return "Scalpel";
            case INSTRUMENT_TAG.SUTURE_SCISSOR:
                return "Suture Scissor";
            case INSTRUMENT_TAG.TOWEL_CLAMPS:
                return "Towel Clamps";
            case INSTRUMENT_TAG.NONE:
                return "None";
            default:
                throw new System.Exception("Name for instrument tag " + tag + " does not exist.");
        }
    }

    /// <summary>
    /// Get instrument enum from name
    /// </summary>
    /// <param name="str">The instrument name</param>
    /// <returns>The instrument enum value</returns>
    public static INSTRUMENT_TAG GetInstrumentTagFromString(string str)
    {
        switch (str)
        {
            case "Addson-Brown Forceps":
                return INSTRUMENT_TAG.ADDSON_BROWN_FORCEPS;
            case "Mayo Hegar Needle Driver":
                return INSTRUMENT_TAG.MAYO_HEGAR_NEEDLE_DRIVER;
            case "Mayo Scissors":
                return INSTRUMENT_TAG.MAYO_SCISSOR;
            case "Metzembaum Scissors":
                return INSTRUMENT_TAG.METZEMBAUM_SCISSOR;
            case "Rochester Carmalt Forceps":
                return INSTRUMENT_TAG.ROCHESTER_CARMALT_FORCEPS;
            case "Scalpel":
                return INSTRUMENT_TAG.SCALPEL;
            case "Suture Scissors":
                return INSTRUMENT_TAG.SUTURE_SCISSOR;
            case "Towel Clamps":
                return INSTRUMENT_TAG.TOWEL_CLAMPS;
            case "Empty":
                return INSTRUMENT_TAG.NONE;
            default:
                throw new System.Exception("Enum for instrument " + str + " does not exist.");
        }
    }

    /// <summary>
    /// Get the description of the purpose of an instrument
    /// </summary>
    /// <param name="tag">Instrument tag</param>
    /// <returns>The description of the purpose</returns>
    public static string GetPurposeDescription(INSTRUMENT_TAG tag)
    {
        switch (tag)
        {
            case INSTRUMENT_TAG.ADDSON_BROWN_FORCEPS:
                return "Traction and manipulation of tissues.";
            case INSTRUMENT_TAG.MAYO_HEGAR_NEEDLE_DRIVER:
                return "Suturing, placement of ligatures";
            case INSTRUMENT_TAG.MAYO_SCISSOR:
                return "Extension of abdominal wall incisions";
            case INSTRUMENT_TAG.METZEMBAUM_SCISSOR:
                return "Blunt dissection of subcutaneous tissues";
            case INSTRUMENT_TAG.ROCHESTER_CARMALT_FORCEPS:
                return "Crushing of vascular pedicles";
            case INSTRUMENT_TAG.SCALPEL:
                return "Skin and abdominal incision, transection of ligated pedicles";
            case INSTRUMENT_TAG.SUTURE_SCISSOR:
                return "Cutting of suture";
            case INSTRUMENT_TAG.TOWEL_CLAMPS:
                return "Joining drapes to patient";
            default:
                throw new System.Exception("Purpose description for instrument tag "+tag+" does not exist.");
        }
    }
}
