// WARNING: T4 generated file  (it is related to CodeToServerProxy)
//

/// <reference path="../Scaffold/Scripts/typings/jquery/jquery.d.ts"/>

module Scaffold {
    export class AjaxSettings {
        async = true;
        cache = false;
        timeout = 2000;

        public build(settings: JQueryAjaxSettings): JQueryAjaxSettings{
            return {
                async: this.async,
                cache: this.cache,
                timeout: this.timeout,
                dataType: 'json',
				contentType: 'application/json',
			    type: settings.type,
			    url: settings.url,
                data: settings.data
            }
        }

    }
}

module App.Models {
    export interface IAnu {
        ID: number;
        Satu: number;
        Dua: number;
    }

    export class Anu {
        public static ajaxSettings = new Scaffold.AjaxSettings();
        ID: number;
        Satu: number;
        Dua: number;
        constructor(data?: IAnu) {
            this.ID = data ? data.ID : null;
            this.Satu = data ? data.Satu : null;
            this.Dua = data ? data.Dua : null;
        }

        /* App.Controllers.AnuController */

        static GetAll(): JQueryPromise<Array<Anu>> {
            var res = $.ajax(Anu.ajaxSettings.build({
                type: 'GET',
                url: '/api/Anu/GetAll',
            })).then((models) => {
                return models.map((model) => new Anu(model));
            });
            return res;
        }

        static Get(id: number): JQueryPromise<Anu> {
            var res = $.ajax(Anu.ajaxSettings.build({
                type: 'GET',
                url: '/api/Anu/Get/'+id,
            })).then((model) => new Anu(model));
            return res;
        }
                
        Save(): JQueryPromise<void> {
            var isNew = this.ID == null;
            var model = this;
            var res = $.ajax(Anu.ajaxSettings.build({
                type: isNew ? 'POST' : 'PUT',
                url: '/api/Anu/'+(isNew ? 'Post' : 'Put'),
                data: JSON.stringify(this)
            })).then((id) => {
                if(isNew){
                    this.ID = id;
                }
            });
            return res;
        }

        Delete(): JQueryPromise<void> {
            var res = $.ajax(Anu.ajaxSettings.build({
                type: 'DELETE',
                url: '/api/Anu/Delete/'+this.ID,
            }));
            return res;
        }

        static Delete(id: number): JQueryPromise<void> {
            var res = $.ajax(Anu.ajaxSettings.build({
                type: 'GET',
                url: '/api/Anu/Delete/'+id,
            }));
            return res;
        }
                
        PostCount(): JQueryPromise<number> {
            var res = $.ajax(Anu.ajaxSettings.build({
                type: 'POST',
                url: '/api/Anu/PostCount',
                data: JSON.stringify(this),
            }));
            return res;
        }
    }

    export interface IModel2 {
        ID: number;
        Description: string;
    }

    export class Model2 {
        public static ajaxSettings = new Scaffold.AjaxSettings();
        ID: number;
        Description: string;
        constructor(data?: IModel2) {
            this.ID = data ? data.ID : null;
            this.Description = data ? data.Description : null;
        }
    }

    export interface ITodo {
        ID: number;
        Name: string;
        Description: string;
    }

    export class Todo {
        public static ajaxSettings = new Scaffold.AjaxSettings();
        ID: number;
        Name: string;
        Description: string;
        constructor(data?: ITodo) {
            this.ID = data ? data.ID : null;
            this.Name = data ? data.Name : null;
            this.Description = data ? data.Description : null;
        }

        /* App.Controllers.TodoController */

        static GetAll(): JQueryPromise<Array<Todo>> {
            var res = $.ajax(Todo.ajaxSettings.build({
                type: 'GET',
                url: '/api/Todo/GetAll',
            })).then((models) => {
                return models.map((model) => new Todo(model));
            });
            return res;
        }

        static Get(id: number): JQueryPromise<Todo> {
            var res = $.ajax(Todo.ajaxSettings.build({
                type: 'GET',
                url: '/api/Todo/Get/'+id,
            })).then((model) => new Todo(model));
            return res;
        }
                
        Save(): JQueryPromise<void> {
            var isNew = this.ID == null;
            var model = this;
            var res = $.ajax(Todo.ajaxSettings.build({
                type: isNew ? 'POST' : 'PUT',
                url: '/api/Todo/'+(isNew ? 'Post' : 'Put'),
                data: JSON.stringify(this)
            })).then((id) => {
                if(isNew){
                    this.ID = id;
                }
            });
            return res;
        }

        Delete(): JQueryPromise<void> {
            var res = $.ajax(Todo.ajaxSettings.build({
                type: 'DELETE',
                url: '/api/Todo/Delete/'+this.ID,
            }));
            return res;
        }

        static Delete(id: number): JQueryPromise<void> {
            var res = $.ajax(Todo.ajaxSettings.build({
                type: 'GET',
                url: '/api/Todo/Delete/'+id,
            }));
            return res;
        }
                
        static GetAduh(): JQueryPromise<number> {
            var res = $.ajax(Todo.ajaxSettings.build({
                type: 'GET',
                url: '/api/Todo/GetAduh',
            }));
            return res;
        }

        PostBody(): JQueryPromise<number> {
            var res = $.ajax(Todo.ajaxSettings.build({
                type: 'POST',
                url: '/api/Todo/PostBody',
                data: JSON.stringify(this),
            }));
            return res;
        }

        static PostBody2(/** [FromBody] **/todo: App.Models.IModel2): JQueryPromise<number> {
            var res = $.ajax(Todo.ajaxSettings.build({
                type: 'POST',
                url: '/api/Todo/PostBody2',
                data: JSON.stringify(todo),
            }));
            return res;
        }
    }

}
