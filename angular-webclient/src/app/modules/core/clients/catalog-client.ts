﻿/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.2.5.0 (NJsonSchema v10.1.7.0 (Newtonsoft.Json v11.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const CATALOG_API_URL = new InjectionToken<string>('CATALOG_API_URL');

@Injectable()
export class CatalogClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(CATALOG_API_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    /**
     * Get a specific product by id
     * @param api_version (optional) 
     * @return Success
     */
    products(id: string | null, api_version: string | undefined): Observable<ProductDetail> {
        let url_ = this.baseUrl + "/products/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id)); 
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",			
            headers: new HttpHeaders({
                "api-version": api_version !== undefined && api_version !== null ? "" + api_version : "", 
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processProducts(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processProducts(<any>response_);
                } catch (e) {
                    return <Observable<ProductDetail>><any>_observableThrow(e);
                }
            } else
                return <Observable<ProductDetail>><any>_observableThrow(response_);
        }));
    }

    protected processProducts(response: HttpResponseBase): Observable<ProductDetail> {
        const status = response.status;
        const responseBlob = 
            response instanceof HttpResponse ? response.body : 
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }};
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = ProductDetail.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status === 404) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result404: any = null;
            let resultData404 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("Not Found", status, _responseText, _headers, result404);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ProductDetail>(<any>null);
    }

    /**
     * Gets all featured products
     * @param api_version (optional) 
     * @return Success
     */
    featuredproducts(api_version: string | undefined): Observable<FeaturedProduct[]> {
        let url_ = this.baseUrl + "/featuredproducts";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",			
            headers: new HttpHeaders({
                "api-version": api_version !== undefined && api_version !== null ? "" + api_version : "", 
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processFeaturedproducts(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processFeaturedproducts(<any>response_);
                } catch (e) {
                    return <Observable<FeaturedProduct[]>><any>_observableThrow(e);
                }
            } else
                return <Observable<FeaturedProduct[]>><any>_observableThrow(response_);
        }));
    }

    protected processFeaturedproducts(response: HttpResponseBase): Observable<FeaturedProduct[]> {
        const status = response.status;
        const responseBlob = 
            response instanceof HttpResponse ? response.body : 
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }};
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(FeaturedProduct.fromJS(item));
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<FeaturedProduct[]>(<any>null);
    }

    /**
     * Gets all categories
     * @param api_version (optional) 
     * @return Success
     */
    categories(api_version: string | undefined): Observable<CategoryTreeItem[]> {
        let url_ = this.baseUrl + "/categories";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",			
            headers: new HttpHeaders({
                "api-version": api_version !== undefined && api_version !== null ? "" + api_version : "", 
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processCategories(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processCategories(<any>response_);
                } catch (e) {
                    return <Observable<CategoryTreeItem[]>><any>_observableThrow(e);
                }
            } else
                return <Observable<CategoryTreeItem[]>><any>_observableThrow(response_);
        }));
    }

    protected processCategories(response: HttpResponseBase): Observable<CategoryTreeItem[]> {
        const status = response.status;
        const responseBlob = 
            response instanceof HttpResponse ? response.body : 
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }};
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(CategoryTreeItem.fromJS(item));
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<CategoryTreeItem[]>(<any>null);
    }
}

/** Brand Model */
export class Brand implements IBrand {
    id?: string | undefined;
    name?: string | undefined;

    constructor(data?: IBrand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
        }
    }

    static fromJS(data: any): Brand {
        data = typeof data === 'object' ? data : {};
        let result = new Brand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        return data; 
    }
}

/** Brand Model */
export interface IBrand {
    id?: string | undefined;
    name?: string | undefined;
}

export class Copy implements ICopy {
    description?: string | undefined;
    notes?: string | undefined;
    bullets?: string[] | undefined;

    constructor(data?: ICopy) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.description = _data["description"];
            this.notes = _data["notes"];
            if (Array.isArray(_data["bullets"])) {
                this.bullets = [] as any;
                for (let item of _data["bullets"])
                    this.bullets!.push(item);
            }
        }
    }

    static fromJS(data: any): Copy {
        data = typeof data === 'object' ? data : {};
        let result = new Copy();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["description"] = this.description;
        data["notes"] = this.notes;
        if (Array.isArray(this.bullets)) {
            data["bullets"] = [];
            for (let item of this.bullets)
                data["bullets"].push(item);
        }
        return data; 
    }
}

export interface ICopy {
    description?: string | undefined;
    notes?: string | undefined;
    bullets?: string[] | undefined;
}

export class Price implements IPrice {
    currency?: string | undefined;
    value?: number;

    constructor(data?: IPrice) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.currency = _data["currency"];
            this.value = _data["value"];
        }
    }

    static fromJS(data: any): Price {
        data = typeof data === 'object' ? data : {};
        let result = new Price();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["currency"] = this.currency;
        data["value"] = this.value;
        return data; 
    }
}

export interface IPrice {
    currency?: string | undefined;
    value?: number;
}

export class Image implements IImage {
    smallUrl?: string | undefined;
    largeUrl?: string | undefined;

    constructor(data?: IImage) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.smallUrl = _data["smallUrl"];
            this.largeUrl = _data["largeUrl"];
        }
    }

    static fromJS(data: any): Image {
        data = typeof data === 'object' ? data : {};
        let result = new Image();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["smallUrl"] = this.smallUrl;
        data["largeUrl"] = this.largeUrl;
        return data; 
    }
}

export interface IImage {
    smallUrl?: string | undefined;
    largeUrl?: string | undefined;
}

export class Category implements ICategory {
    id?: string | undefined;
    name?: string | undefined;

    constructor(data?: ICategory) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
        }
    }

    static fromJS(data: any): Category {
        data = typeof data === 'object' ? data : {};
        let result = new Category();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        return data; 
    }
}

export interface ICategory {
    id?: string | undefined;
    name?: string | undefined;
}

/** Product Model */
export class ProductDetail implements IProductDetail {
    id?: number;
    title?: string | undefined;
    shortDescription?: string | undefined;
    brand?: Brand;
    copy?: Copy;
    price?: Price;
    salePrice?: Price;
    primaryImage?: Image;
    additionalImages?: Image[] | undefined;
    categories?: Category[] | undefined;

    constructor(data?: IProductDetail) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.title = _data["title"];
            this.shortDescription = _data["shortDescription"];
            this.brand = _data["brand"] ? Brand.fromJS(_data["brand"]) : <any>undefined;
            this.copy = _data["copy"] ? Copy.fromJS(_data["copy"]) : <any>undefined;
            this.price = _data["price"] ? Price.fromJS(_data["price"]) : <any>undefined;
            this.salePrice = _data["salePrice"] ? Price.fromJS(_data["salePrice"]) : <any>undefined;
            this.primaryImage = _data["primaryImage"] ? Image.fromJS(_data["primaryImage"]) : <any>undefined;
            if (Array.isArray(_data["additionalImages"])) {
                this.additionalImages = [] as any;
                for (let item of _data["additionalImages"])
                    this.additionalImages!.push(Image.fromJS(item));
            }
            if (Array.isArray(_data["categories"])) {
                this.categories = [] as any;
                for (let item of _data["categories"])
                    this.categories!.push(Category.fromJS(item));
            }
        }
    }

    static fromJS(data: any): ProductDetail {
        data = typeof data === 'object' ? data : {};
        let result = new ProductDetail();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["title"] = this.title;
        data["shortDescription"] = this.shortDescription;
        data["brand"] = this.brand ? this.brand.toJSON() : <any>undefined;
        data["copy"] = this.copy ? this.copy.toJSON() : <any>undefined;
        data["price"] = this.price ? this.price.toJSON() : <any>undefined;
        data["salePrice"] = this.salePrice ? this.salePrice.toJSON() : <any>undefined;
        data["primaryImage"] = this.primaryImage ? this.primaryImage.toJSON() : <any>undefined;
        if (Array.isArray(this.additionalImages)) {
            data["additionalImages"] = [];
            for (let item of this.additionalImages)
                data["additionalImages"].push(item.toJSON());
        }
        if (Array.isArray(this.categories)) {
            data["categories"] = [];
            for (let item of this.categories)
                data["categories"].push(item.toJSON());
        }
        return data; 
    }
}

/** Product Model */
export interface IProductDetail {
    id?: number;
    title?: string | undefined;
    shortDescription?: string | undefined;
    brand?: Brand;
    copy?: Copy;
    price?: Price;
    salePrice?: Price;
    primaryImage?: Image;
    additionalImages?: Image[] | undefined;
    categories?: Category[] | undefined;
}

export class ProblemDetails implements IProblemDetails {
    type?: string | undefined;
    title?: string | undefined;
    status?: number | undefined;
    detail?: string | undefined;
    instance?: string | undefined;

    constructor(data?: IProblemDetails) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.type = _data["type"];
            this.title = _data["title"];
            this.status = _data["status"];
            this.detail = _data["detail"];
            this.instance = _data["instance"];
        }
    }

    static fromJS(data: any): ProblemDetails {
        data = typeof data === 'object' ? data : {};
        let result = new ProblemDetails();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["type"] = this.type;
        data["title"] = this.title;
        data["status"] = this.status;
        data["detail"] = this.detail;
        data["instance"] = this.instance;
        return data; 
    }
}

export interface IProblemDetails {
    type?: string | undefined;
    title?: string | undefined;
    status?: number | undefined;
    detail?: string | undefined;
    instance?: string | undefined;
}

export class FeaturedProduct implements IFeaturedProduct {
    id?: number;
    title?: string | undefined;
    shortDescription?: string | undefined;
    price?: Price;
    salePrice?: Price;
    primaryImage?: Image;

    constructor(data?: IFeaturedProduct) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.title = _data["title"];
            this.shortDescription = _data["shortDescription"];
            this.price = _data["price"] ? Price.fromJS(_data["price"]) : <any>undefined;
            this.salePrice = _data["salePrice"] ? Price.fromJS(_data["salePrice"]) : <any>undefined;
            this.primaryImage = _data["primaryImage"] ? Image.fromJS(_data["primaryImage"]) : <any>undefined;
        }
    }

    static fromJS(data: any): FeaturedProduct {
        data = typeof data === 'object' ? data : {};
        let result = new FeaturedProduct();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["title"] = this.title;
        data["shortDescription"] = this.shortDescription;
        data["price"] = this.price ? this.price.toJSON() : <any>undefined;
        data["salePrice"] = this.salePrice ? this.salePrice.toJSON() : <any>undefined;
        data["primaryImage"] = this.primaryImage ? this.primaryImage.toJSON() : <any>undefined;
        return data; 
    }
}

export interface IFeaturedProduct {
    id?: number;
    title?: string | undefined;
    shortDescription?: string | undefined;
    price?: Price;
    salePrice?: Price;
    primaryImage?: Image;
}

export class CategoryTreeItem implements ICategoryTreeItem {
    id?: number;
    name?: string | undefined;
    children?: CategoryTreeItem[] | undefined;

    constructor(data?: ICategoryTreeItem) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            if (Array.isArray(_data["children"])) {
                this.children = [] as any;
                for (let item of _data["children"])
                    this.children!.push(CategoryTreeItem.fromJS(item));
            }
        }
    }

    static fromJS(data: any): CategoryTreeItem {
        data = typeof data === 'object' ? data : {};
        let result = new CategoryTreeItem();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        if (Array.isArray(this.children)) {
            data["children"] = [];
            for (let item of this.children)
                data["children"].push(item.toJSON());
        }
        return data; 
    }
}

export interface ICategoryTreeItem {
    id?: number;
    name?: string | undefined;
    children?: CategoryTreeItem[] | undefined;
}

export class ApiException extends Error {
    message: string;
    status: number; 
    response: string; 
    headers: { [key: string]: any; };
    result: any; 

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new ApiException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader(); 
            reader.onload = event => { 
                observer.next((<any>event.target).result);
                observer.complete();
            };
            reader.readAsText(blob); 
        }
    });
}