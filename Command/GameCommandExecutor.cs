using System;
using System.Collections.Generic;
using UnityEngine;

public class GameCommandExecutor : MonoBehaviour
{
    public static GameCommandExecutor instance;
    
    private List<GameCommand> m_Commands = new List<GameCommand>();

    private void Awake()
    {
        if (!ReferenceEquals(instance, null))
            Destroy(instance);

        instance = this;
    }

    private void Update()
    {
        Execute();
    }

    public void AddCommand(GameCommand command)
    {
        m_Commands.Add(command);
    }

    private void Execute()
    {
        foreach (var command in m_Commands)
        {
            command.Execute();
        }
        
        m_Commands.Clear();
    }
}
