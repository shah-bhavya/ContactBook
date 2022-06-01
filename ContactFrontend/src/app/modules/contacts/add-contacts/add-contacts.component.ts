import { HttpClient, HttpEventType, HttpRequest } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ContactService } from 'src/app/core/services/contact/contact.service';
import { IContactRequestModel } from 'src/app/shared/models/contactRequestModel';
import { environment } from 'src/environments/environment';
import { ContactType } from 'src/app/shared/Enums/ContactType.enum';

@Component({
  selector: 'app-add-contacts',
  templateUrl: './add-contacts.component.html',
  styleUrls: ['./add-contacts.component.scss']
})
export class AddContactsComponent implements OnInit {

  contactForm: FormGroup;
  submitted = false;
  Id = 0;
  formData = new FormData();
  contactTypeArr = ContactType;
  isEdit = false;
  deletedPhones = [];
  contactTypes = Object.keys(this.contactTypeArr).filter(f => !isNaN(Number(f)));
  imagePath = environment.apiURL + "Resources/Images/"
  imageSrc: string = 'https://t3.ftcdn.net/jpg/03/46/83/96/360_F_346839683_6nAPzbhpSkIpb8pmAwufkC7c5eD7wYws.jpg';

  constructor(private contactService: ContactService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute) {}

  ngOnInit(): void {
    
    this.Id = this.route.snapshot.params['id'];
    this.formData = new FormData();
    this.contactForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      title: [''],
      email: ['', [Validators.email]],
      profilePhoto: [''],
      phones: this.fb.array([
        this.addPhonesFormGroup()
      ])
    });
    
    if (this.Id) {
      this.isEdit = true;
      
      this.getContact(this.Id);
      
    }
  }

  get f() {
    return this.contactForm.controls;
  }

  addPhonesFormGroup(): FormGroup {

    return this.fb.group({
      phoneId: [''],
      phone: ['', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]],
      phoneType: ['', Validators.required],
    });
  }

  addButtonClick(): void {
    (<FormArray>this.contactForm.get('phones')).push(this.addPhonesFormGroup());
  }

  getPhonesFormControls(): AbstractControl[] {
    return (<FormArray>this.contactForm.get('phones')).controls;
  }

  removePhone(index) {
    var arrayControl = this.contactForm.get('phones') as FormArray;

    if (this.isEdit) {
      var item = arrayControl.at(index).value;
     
      this.deletedPhones.push(item.phoneId);
      arrayControl.removeAt(index);
    }
    else{
      arrayControl.removeAt(index);
    }
    


  }

  submitForm(): void {

    this.formData.append("name", this.contactForm.get('name').value);
    this.formData.append("phoneStr", JSON.stringify(this.contactForm.get('phones').value));
    this.formData.append("email", this.contactForm.get('email').value);
    this.formData.append("title", this.contactForm.get('title').value);


    this.submitted = true;
    if (this.contactForm.invalid) {
      return;
    }

    if (!this.isEdit) {

      this.contactService.CreateContacts(this.formData).subscribe(data => {
        this.router.navigate(['']);
      });
    } else {
      this.formData.append("photoPath", this.contactForm.get('profilePhoto').value);
      this.formData.append("removedPhonesStr", JSON.stringify(this.deletedPhones));

      this.contactService.UpdateContacts(this.Id, this.formData).subscribe(data => {
        this.router.navigate(['']);
      });
    }
  }

  getContact(id) {
    this.removePhone(0);
    this.contactService.GetContactById(id).subscribe(data => {
     
      this.contactForm.patchValue(data);
      this.deletedPhones = [];
      this.contactForm.get('profilePhoto').patchValue(data.photoPath);
      data.phone.split(";").forEach(element => {
        (<FormArray>this.contactForm.get('phones')).push(this.fillDynamicControl(element));
      });


      this.imageSrc = this.imagePath + data.photoPath;
    });
  }

  fillDynamicControl(data) {
    var coreData = data.split("//");
    return this.fb.group({
      phoneId: [coreData[0]],
      phoneType: [coreData[1], Validators.required],
      phone: [coreData[2], [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]]
    });
  }

  onFileChange(event: any) {
    const reader = new FileReader();

    if (event.target.files && event.target.files.length) {

      const [file] = event.target.files;
      let fileToUpload = event.target.files[0];
      reader.readAsDataURL(file);

      reader.onload = () => {

        this.imageSrc = reader.result as string;
        this.formData.append("file", fileToUpload, fileToUpload.name);
      };

    }
  }
}
