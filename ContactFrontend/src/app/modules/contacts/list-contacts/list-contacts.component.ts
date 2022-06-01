import { Component, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ContactService } from 'src/app/core/services/contact/contact.service';
import { environment } from 'src/environments/environment';
import { FormsModule } from '@angular/forms';
import { PopupService } from 'src/app/core/services/contact/popup.service';
import { createCustomElement } from '@angular/elements';
import { PopUpAddComponent } from '../popup-add-contact/popup-add-contact.component';

@Component({
  selector: 'app-list-contacts',
  templateUrl: './list-contacts.component.html',
  styleUrls: ['./list-contacts.component.scss']
})
export class ListContactsComponent implements OnInit {

  Contacts:any = [];
  AllContacts:any=[];
  page: number = 1;
  imagePath = environment.apiURL + "Resources/Images/";
  searchTerm = "";
  imageSrc: string = 'https://t3.ftcdn.net/jpg/03/46/83/96/360_F_346839683_6nAPzbhpSkIpb8pmAwufkC7c5eD7wYws.jpg';
  color = 'red';
  

  constructor(private contactService: ContactService, 
    injector: Injector, public popup: PopupService) {
      const PopupElement = createCustomElement(PopUpAddComponent, {injector});
      if(PopupElement.length > 0)
        return;
      customElements.define('popup-element', PopupElement);
     }

  ngOnInit(): void {
    this.getAllContacts();
  }

  getAllContacts()
  {
    this.contactService.GetAllContacts().subscribe((data: {}) => {
      this.Contacts = data;
      this.AllContacts = this.Contacts;
    }); 
  }

  deleteContact(id)
  {
    this.contactService.DeleteContacts(id).subscribe((data: {}) => {
      this.getAllContacts();
    }); 
  }

  search(value: string): void {
    this.Contacts = this.AllContacts.filter((val) =>
      val.name.toLowerCase().includes(value) ||
      val.phone.includes(value)
    );
  }

  onFavClicked(isFav, id): void {
    
    this.contactService.UpdateFavourite(id).subscribe(data=>{
      this.Contacts.find(x=>x.id === id).isFavourite = !(isFav === 'true');

    });
    
  }
}
  