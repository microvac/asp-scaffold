// WARNING: T4 generated file  (it is related to CodeToServerProxy)
//

module App.Models {

    // C:\Workspace\vstudio\Scaffold\Scaffold.Web\Models\Todo.cs: 10
    export interface ITodo {
        Name: string;
        Description: string;
    }

    export class Todo {
        Name: string;
        Description: string;
        constructor(data?: ITodo) {
            this.Name = data ? data.Name : null;
            this.Description = data ? data.Description : null;
        }
    }

}