export class CartCreateCondition{
    PetDetailId: number;
    Quantity: number;

    constructor(){
        this.PetDetailId = 0;
        this.Quantity = 1;
    }
}

export class CartUpdateCondition{
    CartItemId: number;
    PetDetailId: number;
    Quantity: number;

    constructor(){
        this.CartItemId = 0;
        this.PetDetailId = 0;
        this.Quantity = 0;
    }
}

export class CartItemList{
    Id: number;
    CartItemId: number;
    CartId: number;
    PetDetailId: number;
    PetTitle: string;
    Price: number;
    Discount: number;
    PriceDiscount: number;
    Quantity: number;
    PetImage: string;
    TotalPriceItem: number;

    QuantityPet: number;
}

export class CartItem{
    PetDetailId: number;
    Quantity: number;

    constructor(){
        this.PetDetailId =0;
        this.Quantity = 0;
    }
}