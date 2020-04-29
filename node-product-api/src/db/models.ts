export interface Product
{
    id: number;
    itemNumber: string;
    title: string;
    shortDescription: string;
    retailPrice: number;
    retailCurrency: string;
    smallImageUrl: string;
    largeImageUrl: string;
    brandId: number;
}

export interface Brand
{
    id: number;
    name: string;
}