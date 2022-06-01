import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-favourite-contacts',
  templateUrl: './favourite-contacts.component.html',
  styleUrls: ['./favourite-contacts.component.scss']
})
export class FavouriteContactsComponent implements OnInit {

  @Input() isFavourite : Boolean = false;
  @Output() favouriteClicked: EventEmitter<string> = new EventEmitter<string>();
  
  constructor() { }

  ngOnInit(): void {
  }

  onClick()
  {
    this.favouriteClicked.emit(``+this.isFavourite);
    //this.isFavourite = !this.isFavourite;
  }

}
