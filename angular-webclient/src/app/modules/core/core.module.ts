import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';

import { Environment } from '../../../environments/environment';
import { CatalogClient, CATALOG_API_URL } from './clients/catalog-client';

@NgModule({
  declarations: [],
  providers: [
    CatalogClient,
    {
      provide: CATALOG_API_URL,
      useValue: Environment.CATALOG_API_URL
    }
  ],
  imports: [
    CommonModule
  ],
  exports: [

  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    this.throwIfAlreadyLoaded(parentModule, 'CoreModule');
  }

  throwIfAlreadyLoaded(parentModule: any, moduleName: string) {
    if (parentModule) {
      throw new Error(`${moduleName} has already been loaded. Import Core moudles in the AppModule only.`);
    }
  }
}
