using System.Collections.Generic;
using UnityEngine;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using System.IO;
using System;
using System.Threading.Tasks;

public class GoogleSheet : MonoBehaviour
{
    [SerializeField] SOWave so_wave;
    static string sApplicationName = "Google Sheets API .NET Quickstart";

    static string sSpreadsheetId = "1xeYWLxXv5LzyMn6j3TLh9T74tH4imGxpFXLHpAtlWqY";

    static string sSheetName = "Data";

    static SheetsService OpenSheet()
    {
        GoogleCredential credential;
        //authentication process. If credPath is not created because the Browser is activated authentication page opens proceeding by certifying 
        using (var stream = new FileStream(Application.streamingAssetsPath + "/projet5-334616-ad1b8ae12d15.json", FileMode.Open, FileAccess.Read))
        {
            //Credential files are stored in CredPath (modified as follows individual code) 
            credential = GoogleCredential.FromStream(stream).CreateScoped(SheetsService.Scope.Spreadsheets);
        }
        var service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = sApplicationName,

        });
        return service;
    }

    public async Task<bool> GoogleSheetTask()
    {
        Debug.Log("SheetTask");
        await Task.Run(InGameWrite);
        return true;
    }

    public async Task<bool> CleanGoogleSheetTask()
    {
        Debug.Log("CleanTask");
        await Task.Run(CleanSheet);
        return true;
    }

    /// <summary>
    /// Reset the google sheet name
    /// </summary>
    public void CleanSheet()
    {
        ValueRange requestValueRange;
        var service = OpenSheet();
        string wRange;
        int rowNumber = 2;
        wRange = string.Format("{0}!A{1}:D", sSheetName, rowNumber);
        SpreadsheetsResource.ValuesResource.GetRequest getRequest = service.Spreadsheets.Values.Get(sSpreadsheetId, wRange);
        requestValueRange = getRequest.Execute();
        var values = requestValueRange.Values;
        if (values != null && values.Count > 0)
        {
            foreach(var value in values)
            {
                if(value != null)
                {
                    if(rowNumber == 2)
                    {
                        try
                        {
                            so_wave._maxPlayer = int.Parse(value[2].ToString());
                        }catch(Exception e)
                        {
                            Debug.Log(e);
                        }

                    }
                    ValueRange valueRange = new ValueRange();
                    List<object> oblist = new List<object>() {""};
                    wRange = string.Format("{0}!A{1}:D", sSheetName, rowNumber);
                    valueRange.Values = new List<IList<object>> { oblist };
                    var updateRequest = service.Spreadsheets.Values.Update(valueRange, sSpreadsheetId, wRange);
                    updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                    var uUVR = updateRequest.Execute();
                    rowNumber++;
                }
            }
        }
    }
    
    /// <summary>
    /// Write player that join the game and show a counter
    /// </summary>
    public void InGameWrite()
    {
        var service = OpenSheet();
        string wRange;
        int rowNumber = 2;
        foreach (string player in TwitchChat.instance.players)
        {
            try
            {
                wRange = string.Format("{0}!A{1}:D", sSheetName, rowNumber);
                ValueRange valueRange = new ValueRange();
                valueRange.Range = wRange;
                valueRange.MajorDimension = "ROWS";
                List<object> oblist;
                oblist = new List<object>() { string.Format("{0}", player) };
                valueRange.Values = new List<IList<object>> { oblist };
                var updateRequest = service.Spreadsheets.Values.Update(valueRange, sSpreadsheetId, wRange);
                updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                var uUVR = updateRequest.Execute();
                rowNumber++;
                Debug.Log("ICI ");
            } catch (Exception e)
            {
                Debug.Log(e);
            }

        }
    }
}