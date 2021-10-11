using System.Collections;
using System.Collections.Generic;
using EncodeMy;
using UnityEngine;
#if UNITY_EDITOR
public class LanguageTranslate
{
    public string ConvertChinTrad(string strInput)
    {
        EncodeRobert edControl = new EncodeRobert();
        string strResult = "";
        if (strInput == null)
            return strResult;
        if (strInput.ToString().Length >= 1)
            strResult = edControl.SCTCConvert(ConvertType.Simplified, ConvertType.Traditional, strInput);
        else
            strResult = strInput;
        return strResult;
    }

    public string ConvertChinSimp(string strInput)
    {
        EncodeRobert edControl = new EncodeRobert();
        string strResult = "";
        if (strInput == null)
            return strResult;
        if (strInput.ToString().Length >= 1)
            strResult = edControl.SCTCConvert(ConvertType.Traditional, ConvertType.Simplified, strInput);
        else
            strResult = strInput;
        return strResult;
    }
}
#endif
