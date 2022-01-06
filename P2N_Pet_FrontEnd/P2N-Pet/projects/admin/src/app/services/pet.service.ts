import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';



@Injectable({ providedIn: 'root' })
export class PetService {
    public pet: Observable<any>;

    constructor(private http: HttpClient) { }

    GetListPet(condition) {
        return this.http.post(`${environment.apiUrl}APet/Index`, condition);
    }

    GetDetailPet(Id: string){
        return this.http.get(`${environment.apiUrl}APet/GetDetailPet?Id=${Id}`);
    }

    CreatePet(condition) {
        return this.http.post(`${environment.apiUrl}APet/CreatePet`, condition);
    }

    UpdatePet(condition){
        return this.http.post(`${environment.apiUrl}APet/UpdatePet`, condition);
    }

    DeletePet(Id: string){
        return this.http.post(`${environment.apiUrl}APet/DeletePet?Id=${Id}`, null);
    }

    GetNormalBreed(){
        return this.http.get(`${environment.apiUrl}AData/GetNormalBreedSelection`);
    }

    GetNormalSupplier(){
        return this.http.get(`${environment.apiUrl}AData/GetNormalSupplierSelection`);
    }
}
