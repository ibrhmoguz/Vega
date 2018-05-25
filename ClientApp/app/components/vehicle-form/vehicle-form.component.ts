import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../../services/vehicle.service';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[] = [];
  models: any[] = [];
  features: any[] = [];
  vehicle: any = {};

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicle.makeId = 0;
    this.vehicleService.getMakes().subscribe(makes => {
      this.makes = makes;
      console.log(this.makes);
    })

    this.vehicleService.getFeatures().subscribe(features => {
      this.features = features
      console.log(this.features);
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
