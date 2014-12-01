using UnityEngine;
using System.Collections;

public interface IUserAdministrator : IModel
{
    bool RegisterUser(string userName);
    bool RemoveUser(string userName);
    bool RenameUser(string oldOne, string newOne);

    bool IsEmpty();
}
