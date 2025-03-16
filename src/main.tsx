// start the app always with '/' route
import Banner from "@/components/layout/Banner";
import { Toaster as Sonner } from "@/components/ui/sonner";

import { Toaster } from "@/components/ui/toaster";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { createRoot } from "react-dom/client";
import { BrowserRouter, Route, Routes } from "react-router";

import { TooltipProvider } from "./components/ui/tooltip";

import {
  AuthenticatedRoute,
  GuestRoute,
} from "./components/auth/route-components";
import { ThemeProvider } from "./components/layout/theme-provider";
import { FineProvider } from "./hooks/use-fine";
import "./index.css";
import Index from "./pages";
import LoginPage from "./pages/auth/login";
import LogoutPage from "./pages/auth/logout";
import SignupPage from "./pages/auth/signup";
const queryClient = new QueryClient();

createRoot(document.getElementById("root")!).render(
  <FineProvider>
    <QueryClientProvider client={queryClient}>
      <TooltipProvider>
        <ThemeProvider>
          <BrowserRouter>
            <Routes>
              <Route path="/" element={<Index />} />
              <Route
                path="/login"
                element={<GuestRoute Component={LoginPage} />}
              />
              <Route
                path="/signup"
                element={<GuestRoute Component={SignupPage} />}
              />
              <Route
                path="/logout"
                element={<AuthenticatedRoute Component={LogoutPage} />}
              />
            </Routes>
          </BrowserRouter>
          <Sonner />
          <Toaster />
          <Banner />
        </ThemeProvider>
      </TooltipProvider>
    </QueryClientProvider>
  </FineProvider>
);
