using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthController
{

    public string Name { get; private set; }
    public string Group { get; private set; }

    public void Auth(string name, string group)
    {
        Name = name;
        Group = Name;
    }

}
