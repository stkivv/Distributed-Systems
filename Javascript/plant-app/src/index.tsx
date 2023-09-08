import 'jquery';
import 'popper.js';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css'; 
import 'font-awesome/css/font-awesome.min.css';

import './site.css'
import React from 'react';
import ReactDOM from 'react-dom/client';
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import Root from './routes/Root';
import ErrorPage from './routes/ErrorPage';
import Login from './routes/identity/Login';
import Register from './routes/identity/Register';
import Privacy from './routes/Privacy';
import Info from './routes/identity/Info';


import Plants from './routes/plants/Plants';
import Details from './routes/plants/Details'
import CreatePlant from './routes/plants/Create';
import Manage from './routes/manage/Manage';
import Welcome from './routes/Welcome';
import Index from './routes/admin/Index';

import SizeCategories from './routes/admin/sizeCategories/SizeCategories';
import SizeCreate from './routes/admin/sizeCategories/Create';
import SizeEdit from './routes/admin/sizeCategories/Edit';
import SIzeDelete from './routes/admin/sizeCategories/Delete';

import TagColors from './routes/admin/TagColors/TagColors';
import TagColorCreate from './routes/admin/TagColors/Create';
import TagColorEdit from './routes/admin/TagColors/Edit';
import TagColorDelete from './routes/admin/TagColors/Delete';

import PestTypes from './routes/admin/PestType/PestType';
import PestTypeCreate from './routes/admin/PestType/Create';
import PestTypeEdit from './routes/admin/PestType/Edit';
import PestTypeDelete from './routes/admin/PestType/Delete';

import PestSeverity from './routes/admin/PestSeverity/PestSeverities';
import PestSeverityCreate from './routes/admin/PestSeverity/Create';
import PestSeverityEdit from './routes/admin/PestSeverity/Edit';
import PestSeverityDelete from './routes/admin/PestSeverity/Delete';

import EventType from './routes/admin/EventType/EventType';
import EventTypeCreate from './routes/admin/EventType/Create';
import EventTypeEdit from './routes/admin/EventType/Edit';
import EventTypeDelete from './routes/admin/EventType/Delete';

import ReminderType from './routes/admin/ReminderType/ReminderType';
import ReminderTypeCreate from './routes/admin/ReminderType/Create';
import ReminderTypeEdit from './routes/admin/ReminderType/Edit';
import ReminderTypeDelete from './routes/admin/ReminderType/Delete';

import HistoryEntryType from './routes/admin/HistoryEntryType/HistoryEntryType';
import HistoryEntryTypeCreate from './routes/admin/HistoryEntryType/Create';
import HistoryEntryTypeEdit from './routes/admin/HistoryEntryType/Edit';
import HistoryEntryTypeDelete from './routes/admin/HistoryEntryType/Delete';

const router = createBrowserRouter([
  {
      path: "/",
      element: <Root />,
      errorElement: <ErrorPage />,
      children:  [
        {
          path: "/",
          element: <Welcome />,
        },
        {
          path: "login/",
          element: <Login />,
        },
        {
          path: "register/",
          element: <Register />,
        },
        {
          path: "privacy/:id",
          element: <Privacy />,
        },
        {
          path: "info/",
          element: <Info />,
        },
        {
          path: "plants/",
          element: <Plants />
        },
        {
          path: "plants/create/",
          element: <CreatePlant />
        },
        {
          path: "plants/view/:id",
          element: <Details />
        },
        {
          path: "manage/",
          element: <Manage />
        },
        //=======================================ADMIN====================================
        {
          path: "admin/",
          element: <Index />,
        },


        {          
          path: "admin/sizecategories/",
          element: <SizeCategories />
        },
        {          
          path: "admin/sizecategories/create",
          element: <SizeCreate />
        },
        {           
          path: "admin/sizecategories/edit/:id",
          element: <SizeEdit />
        },
        {          
          path: "admin/sizecategories/delete/:id",
          element: <SIzeDelete />
        },


        {          
          path: "admin/tagcolors/",
          element: <TagColors />
        },
        {          
          path: "admin/tagcolors/create",
          element: <TagColorCreate />
        },
        {           
          path: "admin/tagcolors/edit/:id",
          element: <TagColorEdit />
        },
        {          
          path: "admin/tagcolors/delete/:id",
          element: <TagColorDelete />
        },


        {          
          path: "admin/pesttype/",
          element: <PestTypes />
        },
        {          
          path: "admin/pesttype/create",
          element: <PestTypeCreate />
        },
        {           
          path: "admin/pesttype/edit/:id",
          element: <PestTypeEdit />
        },
        {          
          path: "admin/pesttype/delete/:id",
          element: <PestTypeDelete />
        },


        {          
          path: "admin/pestseverities/",
          element: <PestSeverity />
        },
        {          
          path: "admin/pestseverities/create",
          element: <PestSeverityCreate />
        },
        {           
          path: "admin/pestseverities/edit/:id",
          element: <PestSeverityEdit />
        },
        {          
          path: "admin/pestseverities/delete/:id",
          element: <PestSeverityDelete />
        },


        {          
          path: "admin/eventtypes/",
          element: <EventType />
        },
        {          
          path: "admin/eventtypes/create",
          element: <EventTypeCreate />
        },
        {           
          path: "admin/eventtypes/edit/:id",
          element: <EventTypeEdit />
        },
        {          
          path: "admin/eventtypes/delete/:id",
          element: <EventTypeDelete />
        },


        {          
          path: "admin/remindertypes/",
          element: <ReminderType />
        },
        {          
          path: "admin/remindertypes/create",
          element: <ReminderTypeCreate />
        },
        {           
          path: "admin/remindertypes/edit/:id",
          element: <ReminderTypeEdit />
        },
        {          
          path: "admin/remindertypes/delete/:id",
          element: <ReminderTypeDelete />
        },


        {          
          path: "admin/historyentrytypes/",
          element: <HistoryEntryType />
        },
        {          
          path: "admin/historyentrytypes/create",
          element: <HistoryEntryTypeCreate />
        },
        {           
          path: "admin/historyentrytypes/edit/:id",
          element: <HistoryEntryTypeEdit />
        },
        {          
          path: "admin/historyentrytypes/delete/:id",
          element: <HistoryEntryTypeDelete />
        },
      ]
  },
]);

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);