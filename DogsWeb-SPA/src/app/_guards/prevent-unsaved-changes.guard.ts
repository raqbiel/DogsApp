import { DogEditComponent } from './../dogs/dog-edit/dog-edit.component';
import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';


@Injectable()
export class PreventUnsavedChanges implements CanDeactivate<DogEditComponent>{
  canDeactivate(component: DogEditComponent){
    if(component.editForm.dirty){
      return confirm('Jesteś pewien/pewna, że chcesz kontynuować? Wszystkie niezapisane zmiany zostaną utracone')
    }
    return true;
  }
}
