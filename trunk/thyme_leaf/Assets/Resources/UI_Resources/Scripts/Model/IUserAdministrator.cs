using UnityEngine;
using System.Collections;

public interface IUserAdministrator
{
    bool RegisterUser(string userName);
    bool RemoveUser(string userName);
    bool RenameUser(string oldOne, string newOne, int clickFlag);

    bool IsEmpty();
}
