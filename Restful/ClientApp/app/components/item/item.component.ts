import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Http, Headers, RequestOptions } from '@angular/http';

@Component({
    templateUrl: './item.component.html'
})
export class ItemComponent implements OnInit {

    public person: any;

    public constructor(private http: Http, private route: ActivatedRoute, private location: Location) {
        this.person = {
            "firstname": "",
            "lastname": "",
            "email": ""
        }
    }

    public ngOnInit() {
        this.route.params.subscribe(params => {
            if(params["documentId"]) {
                this.http.get("/api/get?document_id=" + params["documentId"])
                    .subscribe(results => {
                        this.person = results.json();
                        this.person.document_id = params["documentId"];
                    }, error => {
                        console.error(error);
                    });
            }
        });
    }

    public save() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        this.http.post("/api/save", JSON.stringify(this.person), options)
            .subscribe(results => {
                this.location.back();
            }, error => {
                console.error(error);
            });
    }

}
