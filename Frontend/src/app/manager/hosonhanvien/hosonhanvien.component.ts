import { Component, OnDestroy, OnInit } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router';
import { Ethnicity } from 'src/app/components/contents/models/ethnicity-enum.model';
import { ManagerService } from '../services/manager.service';
import {  Subscription } from 'rxjs';
import { PersonalResponse } from '../models/personal-response.model';
import { PersonalEdit } from '../models/edit-personal.model';

import { BenefitPlanResponse } from '../models/benefit-request.model';
declare function validateForm2(): boolean;

@Component({
  selector: 'app-hosonhanvien',
  templateUrl: './hosonhanvien.component.html',
  styleUrls: ['./hosonhanvien.component.css']
})
export class HosonhanvienComponent implements OnInit, OnDestroy {
  id: number;
  isEditMode: boolean = false;
  selectedEthnicity = Object.values(Ethnicity);
  model?: PersonalResponse = new PersonalResponse();
  private routeSubscription?: Subscription;
  private updatePersonalSubscription?: Subscription;
  private addPersonalSubscription?: Subscription;
  Ethnicity: Ethnicity;
  benefits: BenefitPlanResponse[];
  selectBenefitId: number;  

  constructor(
    private managerService: ManagerService,
    private route: ActivatedRoute,
    private router: Router,) {
    this.model = {
      personalId: 0,
      currentFirstName: '',
      currentLastName: '',
      currentMiddleName: '',
      birthDate: new Date(),
      driversLicense: '',
      currentCity: '',
      socialSecurityNumber: '',
      currentAddress1: '',
      currentZip: 0,
      currentMaritalStatus: '',
      shareholderStatus: 0,
      currentCountry: '',
      currentAddress2: '',
      currentGender: '',
      currentPhoneNumber: '',
      currentPersonalEmail: '',
      ethnicity: '',
      benefitPlanName: '',
    }
  }  

  ngOnInit(): void {
    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = +params.get('id')!;
        if (this.id) {
          this.isEditMode = true;
          this.managerService.getPersonalById(this.id).subscribe(
            (data) => {
              this.model = data;
            });
        } else {
          this.isEditMode = false; // No ID, so we're adding a new profile          
        }
      }
    });
    this.managerService.getAllBenefit().subscribe({
      next: (data) => {
        this.benefits = data;
        // Find the benefit plan that matches the plan name from the model
        const matchedBenefit = data.find(benefit => benefit.planName === this.model?.benefitPlanName);
        // Set selectBenefitId if a matching benefit is found
        if (matchedBenefit) {
          this.selectBenefitId = matchedBenefit.benefitPlansId;
        }
      }
    });
  }

  onSubmitForm(): void {
    if (!validateForm2()) {
      console.error('Form validation failed');
      return; // Ngăn chặn việc submit form nếu validation thất bại
    }
    // convert this model to request object
    if (this.isEditMode) {
      if (this.model && this.id) {
        var updatePersonal: PersonalEdit = {
          personalId: this.model.personalId,
          currentFirstName: this.model.currentFirstName,
          currentLastName: this.model.currentLastName,
          currentMiddleName: this.model.currentMiddleName,
          birthDate: this.model.birthDate,
          currentCity: this.model.currentCity,
          driversLicense: this.model.driversLicense,
          socialSecurityNumber: this.model.socialSecurityNumber,
          currentAddress1: this.model.currentAddress1,
          currentZip: this.model.currentZip,
          currentMaritalStatus: this.model.currentMaritalStatus,
          shareholderStatus: this.model.shareholderStatus,
          currentCountry: this.model.currentCountry,
          currentAddress2: this.model.currentAddress2,
          currentGender: this.model.currentGender,
          currentPhoneNumber: this.model.currentPhoneNumber,
          currentPersonalEmail: this.model.currentPersonalEmail,
          ethnicity: this.model.ethnicity,
          benefitPlanId: this.selectBenefitId,
        };
        this.updatePersonalSubscription = this.managerService.updatePersonal(this.id, updatePersonal).subscribe({
          next: (response) => {
            this.router.navigateByUrl('/manage/personal');
          }
        });
      }
    } else {
      if (this.model) {
        var createPersonal: PersonalEdit = {
          personalId: this.model.personalId,
          currentFirstName: this.model.currentFirstName,
          currentLastName: this.model.currentLastName,
          currentMiddleName: this.model.currentMiddleName,
          birthDate: this.model.birthDate,
          currentCity: this.model.currentCity,
          driversLicense: this.model.driversLicense,
          socialSecurityNumber: this.model.socialSecurityNumber,
          currentAddress1: this.model.currentAddress1,
          currentZip: this.model.currentZip,
          currentMaritalStatus: this.model.currentMaritalStatus,
          shareholderStatus: this.model.shareholderStatus,
          currentCountry: this.model.currentCountry,
          currentAddress2: this.model.currentAddress2,
          currentGender: this.model.currentGender,
          currentPhoneNumber: this.model.currentPhoneNumber,
          currentPersonalEmail: this.model.currentPersonalEmail,
          ethnicity: this.model.ethnicity,
          benefitPlanId: this.selectBenefitId,
        };
        console.log(createPersonal);
        this.addPersonalSubscription = this.managerService.createPersonal(createPersonal).subscribe({
          next: (response) => {
            this.router.navigateByUrl('/manage/personal');
          }
        });
      }

    }
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updatePersonalSubscription?.unsubscribe();
    this.addPersonalSubscription?.unsubscribe();
  }
}
