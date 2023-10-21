import { Address } from "./Address";

export class Customer {

  constructor(
    public firstName: string,
    public lastName: string,
    public email: string,
    public phone: string,
    public cpf: string,
    public address: Address
    ){}
  }
  