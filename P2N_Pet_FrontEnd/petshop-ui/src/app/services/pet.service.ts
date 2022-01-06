import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PetService {

  public pet: Observable<any>;

  constructor( private httpClient : HttpClient) { }

  getPetDetail(Id){
    return this.httpClient.get(`${environment.apiUrl}Pet/GetDetailPet?PetDetailId=${Id}`);
  }

  getListPet(condition: any){
    return this.httpClient.post(`${environment.apiUrl}Pet/GetListPet`, condition);
  }

  getMultiPetDetail(condition: any){
    return this.httpClient.post(`${environment.apiUrl}Pet/GetMultiPetDetail`, condition);
  }
}
