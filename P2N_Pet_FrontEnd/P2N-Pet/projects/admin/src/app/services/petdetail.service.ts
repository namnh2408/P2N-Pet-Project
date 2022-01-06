import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';



@Injectable({ providedIn: 'root' })
export class PetDetailService {
    public petDetail: Observable<any>;

    constructor(private http: HttpClient) { }

    GetListPetDetail(condition) {
        return this.http.post(`${environment.apiUrl}APetDetail/Index`, condition);
    }

    GetDetailPetDetail(Id: string){
        return this.http.get(`${environment.apiUrl}APetDetail/GetDetailPetDetail?Id=${Id}`);
    }

    CreatePetDetail(condition) {
        return this.http.post(`${environment.apiUrl}APetDetail/CreatePetDetail`, condition);
    }

    UpdatePetDetail(condition){
        return this.http.post(`${environment.apiUrl}APetDetail/UpdatePetDetail`, condition);
    }

    DeletePetDetail(Id: string){
        return this.http.post(`${environment.apiUrl}APetDetail/DeletePetDetail?Id=${Id}`, null);
    }

    DeletePetDetailImage(petImageId){
        return this.http.post(`${environment.apiUrl}APetDetail/DeletePetDetailImage?petImageId=${petImageId}`, null);
    }

    GetNormalAgeSelection(){
        return this.http.get(`${environment.apiUrl}AData/GetNormalAgeSelection`);
    }

    GetNormalColorSelection(){
        return this.http.get(`${environment.apiUrl}AData/GetNormalColorSelection`);
    }

    GetNormalSizeSelection(){
        return this.http.get(`${environment.apiUrl}AData/GetNormalSizeSelection`);
    }

    GetNormalSexSelection(){
        return this.http.get(`${environment.apiUrl}AData/GetNormalSexSelection`);
    }

    GetNormalBreedPetDetailSelection(supplierid){
        return this.http.get(`${environment.apiUrl}AData/GetNormalBreedPetDetailSelection?supplierid=${supplierid}`);
    }

    GetNormalSupplierPetDetailSelection(breedid){
        return this.http.get(`${environment.apiUrl}AData/GetNormalSupplierPetDetailSelection?breedid=${breedid}`);
    }

    GetNormalStatusDetailSelection(){
        return this.http.get(`${environment.apiUrl}AData/GetNormalStatusDetailSelection`);
    }
}
