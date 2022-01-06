import { User } from 'src/app/models/account';
import { CartItem } from './../../models/cart';
import { AccountService } from './../../services/account.service';
import { PetDetail, PetDetailCondition } from './../../models/petDetail';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PetService } from 'src/app/services/pet.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CartCreateCondition} from 'src/app/models/cart';
import { CartService } from 'src/app/services/cart.service';
import { PetCondition } from 'src/app/models/pet';
import { CartCountService } from 'src/app/services/cartcount.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-single',
  templateUrl: './single.component.html',
  styleUrls: ['./single.component.scss']
})
export class SingleComponent implements OnInit {

  petDetailId: any;
  loading = false;

  form: FormGroup;
  submitted = false;
  countSub: Subscription;

  public petDetail: PetDetail = new PetDetail();

  cartItem: CartItem = new CartItem();
  cartList: Array<CartItem> = [];

  petImageRoot: any;

  cartCondition: CartCreateCondition = new CartCreateCondition();
  petDetailCondition: PetDetailCondition = new PetDetailCondition();

  user: User;
  message: string = "";
  tempQuantity: number;

  petCondition: PetCondition = new PetCondition();
  pets: any;

  quantity: number;

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private petService: PetService,
    private cartService: CartService,
    private accountService: AccountService,
    private cartCountService: CartCountService) { 
      this.tempQuantity = 0;
      this.quantity = 0;
      this.countSub = this.cartCountService.cartCount$.subscribe(
        count => {});
    }

    
  get f() { return this.form.controls; }

  ngOnInit(): void {
    this.router.routeReuseStrategy.shouldReuseRoute = () => {
      return false;
    };

    this.form = this.formBuilder.group({
      Quantity: [1, Validators.required],
    }); 
    
    this.petDetailId = this.route.snapshot.params['id'];
    this.getDetailPet();

    this.petCondition.TopPet = 6;
    this.getList();

    this.getQuantityByPetDetailId(this.petDetailId);

    let target = document.getElementById('target');
    this.scroll(target);
  }

  AddToCart(Id: any){

    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    this.tempQuantity = this.tempQuantity + this.f.Quantity.value;

    if( this.tempQuantity > this.petDetail.Quantity){
      this.message = "Số lượng bạn đặt đã vượt qua số lượng tối đa.";

      this.tempQuantity = this.tempQuantity - this.f.Quantity.value;

      this.loading = false;
      this.submitted = false;

      return;
    }
    else{
      this.message ="";
    }

    if(this.accountService.user){
      this.accountService.user.subscribe( x =>{
        if(x){
          this.user = x;
        }
        else{
          this.user = new User();
        }
      })
    }

    if( !(this.user.Id > 0)){
      this.router.routeReuseStrategy.shouldReuseRoute = () => {
        return false;
      } 
      this.router.onSameUrlNavigation = 'reload';
      this.router.navigate([`/login`]);

      /* sessionStorage.removeItem('cart-pet');

      this.cartItem.PetDetailId = this.petDetail.PetDetailId;
      this.cartItem.Quantity = this.f.Quantity.value;

      this.cartList.push(this.cartItem);
      sessionStorage.setItem('cart-pet', JSON.stringify({...this.cartList}));

      console.log("message : " + this.message);
      console.log("cartList: " + this.cartList);
      console.log("cartItem: " + this.cartItem);

      this.loading = false;
      this.submitted = false; */
    }
    else{
      this.cartCondition.PetDetailId = Id;
      this.cartCondition.Quantity = this.f.Quantity.value;

      this.cartService.AddToCart({...this.cartCondition }).subscribe( (res : any) =>{

        if( res.result == 0){
          this.message = res.message;
        }

        this.cartCountService.getCountQuantity().subscribe((res: any) =>{
          var countQuantity = res.content.countQuantity;
          
          this.cartCountService.setCartCount(countQuantity);
        });

        this.cartService.getQuantityPetDetailIdAndUserId(Id).subscribe((res: any) =>{
          this.quantity = res.content.Quantity;
  
          //let currentUrl = this.router.url;
          this.router.routeReuseStrategy.shouldReuseRoute = () => false;
          this.router.onSameUrlNavigation = 'reload';
          //this.router.navigate([currentUrl]);
        }, error =>{
          this.quantity = 0;
        });
        

        let target = document.getElementById('headerpet1');
        this.scroll(target);

        this.loading = false;
        this.submitted = false;

      }, error => {
        this.loading = false;
        this.submitted = false;
      });
    }
  }


  getDetailPet(){
    this.loading = true;

    this.petService.getPetDetail(this.petDetailId).subscribe((res: any) =>{
      this.petDetail = res.content.PetDetail;

      this.petImageRoot = this.petDetail.petImages[0];
      
      this.petDetailCondition.SizeId = this.petDetail.SizeId;
      this.petDetailCondition.ColorId = this.petDetail.ColorId;
      this.petDetailCondition.AgeId = this.petDetail.AgeId;
      this.petDetailCondition.SexId = this.petDetail.SexId;

      this.loading = false;
    })
  }

  onChangePet(check){
    this.loading = true;

    this.petDetailCondition.PetId = this.petDetail.PetId == undefined ? 0 : this.petDetail.PetId;

    if(check == 1){
      this.petDetailCondition.AgeId = 0;
      this.petDetailCondition.SizeId = 0;
      this.petDetailCondition.SexId = 0;
    }
    else if(check == 2){
      this.petDetailCondition.SizeId = 0;
      this.petDetailCondition.SexId = 0;
    }
    else if(check == 3)
    {
      this.petDetailCondition.SexId = 0;
    }

    this.petService.getMultiPetDetail({...this.petDetailCondition}).subscribe((res: any) =>{
      this.petDetail = res.content.PetDetail;

      this.petImageRoot = this.petDetail.petImages[0];
      this.loading = false;
      
      this.router.routeReuseStrategy.shouldReuseRoute = () => {
        return false;
      } 

      /* this.router.onSameUrlNavigation = 'reload'; */
      this.router.navigate([`/pets/${this.petDetail.PetDetailId}`]);
    });

    let target = document.getElementById('target');
    this.scroll(target);
  }

  refresh(){
    window.location.reload();
  }

  scroll(el: HTMLElement) {
    el.scrollIntoView({behavior: 'smooth'});
  }

  getList(){
    this.loading = true;
    this.petService.getListPet({
      ...this.petCondition
    }).subscribe((res: any) => {
        this.pets = res.content.Pets;
        this.loading = false;
      });
  }

  getQuantityByPetDetailId(Id){
    if(this.accountService.user){
      this.accountService.user.subscribe( x =>{
        if(x){
          this.user = x;
        }
        else{
          this.user = new User();
        }
      })
    }

    if( !(this.user.Id > 0)){
      this.quantity = 0;
    }
    else{
      this.cartService.getQuantityPetDetailIdAndUserId(Id).subscribe((res: any) =>{
        this.quantity = res.content.Quantity;

        //let currentUrl = this.router.url;
        this.router.routeReuseStrategy.shouldReuseRoute = () => false;
        this.router.onSameUrlNavigation = 'reload';
        //this.router.navigate([currentUrl]);
      }, error =>{
        this.quantity = 0;
      });
    }
  }

  changeImage(numImage){
    this.petImageRoot = this.petDetail.petImages[numImage];
  }
}
