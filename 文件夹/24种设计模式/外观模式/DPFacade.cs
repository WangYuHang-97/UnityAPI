using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPFacade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Principal principal = new Principal();
        principal.OrderTeacherToDoTask();;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Principal
{
    private Teacher teacher = new Teacher();

    public void OrderTeacherToDoTask()
    {
        teacher.OrderStudentsToSummary();
    }
}

public class Teacher
{
    private Monitor monitor = new Monitor();
    private LeagueSecretary leagueSecretary = new LeagueSecretary();

    public void OrderStudentsToSummary()
    {
        monitor.WriteSummary();
        leagueSecretary.WriteSummary();
    }
}

public class Monitor
{
    public void WriteSummary()
    {
        Debug.Log("班长的总结");
    }
}

public class LeagueSecretary
{
    public void WriteSummary()
    {
        Debug.Log("团支书的总结");
    }
}