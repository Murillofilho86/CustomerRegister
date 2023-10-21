import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Address } from 'src/app/shared/models/Address';
import { Customer } from 'src/app/shared/models/Customer';
import onlyNumbers from 'src/app/shared/utils/onlyNumbers';
import { environment } from 'src/environments/environment';

interface ZipCodeResponse {
  zipCode: string,
  state: string,
  city: string,
  neighborhood: string,
  street: string
  service: string,
}


@Injectable({
  providedIn: 'root'
})
export class CustomerService {


  constructor(private http: HttpClient) { }

  getAddressByZipCode(cep: string): Observable<ZipCodeResponse> {
   
    const apiUrl = 'https://brasilapi.com.br/api/cep/v2/';

   var req=  this.http.get<ZipCodeResponse>(`${apiUrl}${onlyNumbers(cep)}`);

      return req;
    // return this.http.get<CepResponse>(`${environment.apiUrl}/v1/customer/check-address/${onlyNumbers(cep)}`)
  }

  postRegisterCustomer(customer: Customer){
    const body = JSON.stringify(customer);
    let headers = new HttpHeaders()
    .set("Content-Type", "application/json")
    return this.http.post(`${environment.apiUrl}/customer`, body, {
      headers: headers
    });
  }

}


