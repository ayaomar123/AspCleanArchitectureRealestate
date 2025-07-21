import { CityComponent } from './dashboard/features/pages/city/city.component';
import { Component } from '@angular/core';
import { LoginPageComponent } from './dashboard/features/pages/login-page/login-page.component';
import { Routes } from '@angular/router';
import { HomeLayoutComponent } from './layout/frontend-layout/home-layout/home-layout.component';
import { AdminPageComponent } from './layout/dashboard-layout/pages/admin-page/admin-page.component';
import { AdminHomePageComponent } from './dashboard/features/pages/admin-home-page/admin-home-page.component';
import { CategoryPageComponent } from './dashboard/features/pages/category-page/category-page.component';
import { DistrictComponent } from './dashboard/features/pages/district/district.component';
import { ItemComponent } from './dashboard/features/pages/item/item.component';
import { ItemFormComponent } from './dashboard/features/pages/item-form/item-form.component';
import { ItemDetailsComponent } from './dashboard/features/pages/item-details/item-details.component';

export const routes: Routes = [
    {
        path: '',
        component: HomeLayoutComponent

    },
    {
        path: 'login',
        component: LoginPageComponent

    },
    {
        path: 'admin',
        component: AdminPageComponent,
        children: [
            { path: '', component: AdminHomePageComponent },
            { path: 'categories', component: CategoryPageComponent },
            { path: 'cities', component: CityComponent },
            { path: 'districts', component: DistrictComponent },
            { path: 'items', component: ItemComponent },
            { path: 'items/create', component: ItemFormComponent },
            { path: 'items/:id', component: ItemDetailsComponent },
            { path: 'items/:id/edit', component: ItemFormComponent },
        ]

    },
];
