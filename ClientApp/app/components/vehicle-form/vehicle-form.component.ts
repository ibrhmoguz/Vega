import { Component, OnInit } from '@angular/core';
import { MakeService } from '../../services/make.service';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[] = [];
  models: any[] = [];
  vehicle: any = {};

  constructor(private makeService: MakeService) { }

  ngOnInit() {
    this.vehicle.makeId = 0;
    this.makeService.getMakes().subscribe(makes => {
      this.makes = makes;
      console.log(this.makes);
    })
  }

  onSelectedMakeChanged() {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    if (selectedMake == undefined) {
      this.models = [];
    }
    this.models = selectedMake.models;
  }
}
