import { Component, OnInit, ViewChild } from '@angular/core';
import { Customer } from 'src/app/shared/models/Customer';
import { CustomerService } from '../../core/services/customer.service';
import { NgForm, FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Address } from 'src/app/shared/models/Address';
import { AlertService } from 'src/app/shared/services/alert.service';
import { AlertType } from 'src/app/shared/enum/enum-alerttype';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  // @ts-ignore
  customerForm: FormGroup;


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
      complement: '',
      neighborhood: '',
      city: '',
      state: '',
      zipCode: ''
    },
  };

  ngOnInit(): void {
    this.customerForm = this.formBuilder.group({
      firstName: [null],
      lastName: [null],
      email: [null],
      phone: [null],
      cpf: [null],
      address: this.formBuilder.group({
        street: [null],
        number: [null],
        complement: [null],
        neighborhood: [null],
        city: [null],
        state: [null],
        zipCode: [null],
      })
    });
  }


  onSubmit() {

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
        data.address.complement,
        data.address.neighborhood,
        data.address.city,
        data.address.state,
        data.address.zipCode
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
      // this.alertService.emiteAlertaSimples(
      //   
      // )

      //  

    );

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
        complement: data.complement,
        neighborhood: data.neighborhood,
        city: data.city,
        state: data.state,
      }
    })
  }

}
