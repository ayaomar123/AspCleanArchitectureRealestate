import { Component } from '@angular/core';
import { LoginPageComponent } from './dashboard/features/pages/login-page/login-page.component';
import { Routes } from '@angular/router';
import { HomeLayoutComponent } from './layout/frontend-layout/home-layout/home-layout.component';
import { AdminPageComponent } from './layout/dashboard-layout/pages/admin-page/admin-page.component';
import { AdminHomePageComponent } from './dashboard/features/pages/admin-home-page/admin-home-page.component';

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
            { path: '', component: AdminHomePageComponent }
        ]

    },
];
