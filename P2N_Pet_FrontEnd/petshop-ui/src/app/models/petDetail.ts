export class PetColor{
    ColorId: number;
    ColorName: string;
}

export class PetSize{
    SizeId: number;
    SizeTitle: string;
}

export class PetAge{
    AgeId: number;
    AgeTitle: string;
}

export class PetSex{
    SexId: number;
    SexTitle: string;

    constructor(){
        this.SexId = 0;
        this.SexTitle = "";
    }
}

export class PetDetail{
    PetDetailId : number;
    PetTitle: string;
    PetId: number;
    Content: string;
    BreedId: number;
    BreedName: string;
    SupplierId: number;
    SupplierName: string;
    Quantity: number;
    Price: number;
    Discount: number;
    PriceDiscount: number;
    petImages: string[];
    SizeId: number;
    SizeTitle: string;
    petSizes : PetSize[];
    AgeId: number;
    AgeTitle: string;
    petAges : PetAge[];
    ColorId : number;
    ColorName: string;
    petColors: PetColor[];
    SexId: number;
    SexTitle: string;
    petSexes : PetSex[];
}

export class PetDetailCondition {
    PetDetailId: number;
    PetId: number;
    SizeId: number;
    ColorId: number;
    AgeId: number;
    SexId: number;

    constructor(){
        this.PetDetailId = 0;
        this.PetId = 0;
        this.SizeId = 0;
        this.ColorId = 0;
        this.AgeId = 0;
        this.SexId = 0;
    }
}