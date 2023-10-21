import Swal, { SweetAlertResult } from "sweetalert2"
import { AlertType } from "../enum/enum-alerttype"
import { Injectable } from "@angular/core"
import { MatSnackBar } from "@angular/material/snack-bar"
import { ActionSnack } from "../enum/enum-acaosnack"

const swalWithBootstrapButtons = Swal.mixin({
  customClass: {
    confirmButton: 'btn btn-success',
    cancelButton: 'btn btn-danger'
  },
  buttonsStyling: false
})


@Injectable()
export class AlertService{

  constructor(private snack: MatSnackBar) {}
  
  
  emiteAlertaSimples(tipo:AlertType,titulo:string,mensagem:string){
    return Swal.fire({
      title:titulo,
      text:mensagem,
      icon:tipo,
    })
  }

  emiteAlertaSimplesHtml(tipo:AlertType,tituloHtml:string,mensagemHtml:string){
    Swal.fire({
      title: tituloHtml,
      icon: tipo,
      html:mensagemHtml
    })
  }

  emiteAlertaPerguntaSimples(tipo:AlertType,titulo:string,mensagem:string) : Promise<SweetAlertResult>{
   return swalWithBootstrapButtons.fire({
      title:titulo,
      text:mensagem,
      icon:tipo,
      showCancelButton: true,
      cancelButtonText: 'cancelar',
      confirmButtonText: 'confirmar'
    })
  }

  emiteAlertaLoading(tipo:AlertType,titulo:string,mensagem:string) :  Promise<SweetAlertResult<any>>{
    return swalWithBootstrapButtons.fire({
       title:titulo,
       text:mensagem,
       allowOutsideClick:false,
       allowEscapeKey:false,
       icon:tipo,
       didOpen: () => {
        Swal.showLoading() 
      }
     })
   }

   fecharAlerta(){
    Swal.close();
   }
   emiteAlertSnack(titulo :string,mensagem: string, acao : ActionSnack): void {

    if (acao == ActionSnack.Reconectar) {
      const snackRef = this.snack.open(mensagem, acao);
      snackRef.onAction().subscribe(() => {
        location.reload();
      });
    }
    else {
      const snackRef = this.snack.open(mensagem, titulo, {
        duration: 2000,
      });
    }

  }
}
