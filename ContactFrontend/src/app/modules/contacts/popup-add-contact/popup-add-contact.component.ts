import { animate, state, style, transition, trigger } from "@angular/animations";
import { Component, EventEmitter, HostBinding, Input, OnInit, Output } from "@angular/core";
import { ContactResponseModel } from "src/app/shared/models/contactResponseModel";
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-popup',
    templateUrl: './popup-add-contact.component.html',
    animations: [
        trigger('state', [
            state('opened', style({ transform: 'translateY(0%)' })),
            state('void, closed', style({ transform: 'translateY(100%)', opacity: 0 })),
            transition('* => *', animate('100ms ease-in')),
        ])
    ],
    styles: [`
    :host {
        position: fixed;
        left: 20%;
        top: 10%;
        z-index: 88888888;
        overflow-x: hidden;
        transition: opacity .15s linear;
        align-items: center;
        align-content: center;
        right: 20%;
    }
    .ml-5 {
        margin-left:5px;
    }
    .bi-heart-fill {
        color:red;
    }
    .profile-pic {
        width: 100px;
        border-radius: 50%;
    }
    .modal-header{
        text-align: center;
        display: block;
    }
    .mt-5{
        margin-top:5px;
    }
    .box-shadow{
        box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
    }`]
})

export class PopUpAddComponent implements OnInit {

    public _contactModel;
    imagePath = environment.apiURL + "Resources/Images/";
    imageSrc: string = 'https://t3.ftcdn.net/jpg/03/46/83/96/360_F_346839683_6nAPzbhpSkIpb8pmAwufkC7c5eD7wYws.jpg';

    ngOnInit(): void {
        console.log(this._contactModel);
        this.state = 'opened';

        var element = document.getElementsByTagName("app-list-contacts");
        if (element)
            element[0].classList.add("disabled");
    }

    @HostBinding('@state')
    state: 'opened' | 'closed' = 'closed';

    //@Input()
    // get contactModel(): ContactResponseModel { console.log(this._contactModel); return this._contactModel; }

    // set contactModel(model: ContactResponseModel) {
    //     this._contactModel = model;
    //     this.state = 'opened';
    // }

    @Output()
    closed = new EventEmitter<void>();

    closedPopUp() {
        this.closed.next();
        var element = document.getElementsByTagName("app-list-contacts");
        if (element)
            element[0].classList.remove("disabled");
    }

}