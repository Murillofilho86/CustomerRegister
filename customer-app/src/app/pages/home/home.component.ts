import { Component, OnInit, ViewChild } from '@angular/core';
import { Customer } from 'src/app/shared/models/Customer';
import { CustomerService } from '../../core/services/customer.service';
import { NgForm, FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Address } from 'src/app/shared/models/Address';
import { AlertService } from 'src/app/shared/services/alert.service';
import { AlertType } from 'src/app/shared/enum/enum-alerttype';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  // @ts-ignore
  // @ViewChild('customerForm') customerForm: NgForm;

  public customerForm: FormGroup = this.formBuilder.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phone: ['', Validators.required],
    cpf: ['', Validators.required],
    address: this.formBuilder.group({
      street: ['', Validators.required],
      number: ['', Validators.required],
      complement: [''],
      neighborhood: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      zipCode: ['', Validators.required],
    }),
  });

  constructor(
    private customerService: CustomerService,
    private formBuilder: FormBuilder,
    private alertService: AlertService) {
  }

  customer: Customer = {
    firstName: '',
    lastName: '',
    phone: '',
    cpf: '',
    email: '',
    address: {
      street: '',
      number: '',
      neighborhood: '',
      city: '',
      state: '',
      zipCode: '',
      complement: ''
    },
  };

  ngOnInit(): void {
  } 



  public onSubmit() {

    if (this.customerForm.valid) {

    const data = this.customerForm.value;
    const customer = new Customer(
      data.firstName,
      data.lastName,
      data.email,
      data.phone,
      data.cpf,
      new Address(
        data.address.street,
        data.address.number,
        data.address.neighborhood,
        data.address.city,
        data.address.state,
        data.address.zipCode,
        data.address.complement,
      )
    )

    this.customerService.postRegisterCustomer(customer).subscribe(
      () => {
        this.alertService.emiteAlertaSimples(
          AlertType.Success,
          'Salvo com sucesso',
          ''
        );
        this.customerForm.reset();
      }, (ex) => {
        this.alertService.emiteAlertaSimples(
          AlertType.Error,
          'Erro',
          ex.error.message
        );
      }
     

    );

    }
  }

  searchAddress(zipeCode: string) {

    if (zipeCode != null && zipeCode !== '') {
      this.customerService.getAddressByZipCode(zipeCode).subscribe(
        address => this.setAddressForm(address));
    }
  }

  setAddressForm(data: any) {
    this.customerForm.patchValue({
      address: {
        street: data.street,
        number: data.number,
        neighborhood: data.neighborhood,
        city: data.city,
        state: data.state,
        complement: data.complement
      }
    })
  }

}
