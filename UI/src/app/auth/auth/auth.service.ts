import { Injectable } from '@angular/core';
import { CanActivateFn } from '@angular/router';

export const AuthGuard: CanActivateFn = (route, state) => {
  if (localStorage.getItem('token') != null) {
    return true;
  } else {
    return false;
  }
};
