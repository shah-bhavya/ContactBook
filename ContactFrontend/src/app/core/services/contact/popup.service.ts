import { ApplicationRef, ComponentFactoryResolver, Injectable, Injector } from "@angular/core";
import { PopUpAddComponent } from "src/app/modules/contacts/popup-add-contact/popup-add-contact.component";
import { ContactResponseModel } from "src/app/shared/models/contactResponseModel";

@Injectable()
export class PopupService {
    constructor(private injector: Injector,
        private applicationRef: ApplicationRef,
        private componentFactoryResolver: ComponentFactoryResolver) {}

        showAsComponent(model: ContactResponseModel) {

            // Create element
            const popup = document.createElement('popup-component');
        
            // Create the component and wire it up with the element
            const factory = this.componentFactoryResolver.resolveComponentFactory(PopUpAddComponent);
            const popupComponentRef = factory.create(this.injector, [], popup);
        
            // Attach to the view so that the change detector knows to run
            this.applicationRef.attachView(popupComponentRef.hostView);
        
            // Listen to the close event
            popupComponentRef.instance.closed.subscribe(() => {
              this.applicationRef.detachView(popupComponentRef.hostView);
            });
        
            // Set the model data
            popupComponentRef.instance._contactModel = model;
        
            // Add to the DOM
            document.body.appendChild(popup);
          }
}