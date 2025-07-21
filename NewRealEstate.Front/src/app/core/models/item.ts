import { Booking } from "./booking";
import { Category } from "./category";
import { City } from "./city";
import { District } from "./district";

export interface Item {
    id: number;
    name?: string;
    image?: string;
    status?: boolean;
    advertiseNo: number;

    categoryId: number;
    category?: Category;

    cityId: number;
    city?: City;

    districtId: number;
    district?: District;

    propertyTypeId: number;
    //propertyType?: PropertyType;

    latitude: number;
    longitude: number;
    soum: number;
    limit: number;
    streetWidth: number;
    space: number;
    pricePerMeter: number;

    description?: string;
    hashedPassword?: string;
    createdAt: Date;

    images: Image[];
    bookings?: Booking[];
}

export interface Image {
    id: number;
    imageUrl?: string;
    status: boolean
}
