## API

### Customer
| Name             | Request Url                      | Method |
| ---------------- | -----------------------------    | ------ | 
| GetCustomers     | /api/Customer/GetCustomers       | GET    | 
| GetCustomer      | api/Customer/GetCustomer/:id     | GET    | 
| AddCustomer      | /api/Customer/AddCustomer        | POST   | 
| UpdateCustomer   | /api/Customer/UpdateCustomer/:id | PUT    | 
| DeleteCustomer   | /api/Customer/DeleteCustomer/:id | DELETE | 

## Database Tables

### Table: district_tbl
| Column Name   | Data Type    | Constraints        | Description                      |
|---------------|--------------|--------------------|----------------------------------|
|  districtId   | INT          | PRIMARY KEY        | Unique identifier for districts. |
|  districtName | VARCHAR(45)  | DEFAULT NULL       | Name of the district.            |

### Table: subdistrict_tbl
| Column Name     | Data Type    | Constraints        | Description                         |
|-----------------|--------------|--------------------|-------------------------------------|
| subdistrictId   | INT          | PRIMARY KEY        | Unique identifier for subdistricts. |
| subdistrictName | VARCHAR(45)  | DEFAULT NULL       | Name of the subdistrict.            |

### Table: customer_tbl
| Column Name       | Data Type    | Constraints                  | Description                          |
|-------------------|--------------|------------------------------|--------------------------------------|
| customerid        | INT          | PRIMARY KEY                  | Unique identifier for customers.     |
| customerTitleName | VARCHAR(45)  | DEFAULT NULL                 | Title of the customer (e.g., Mr, Ms).|
| customerFName     | VARCHAR(45)  | DEFAULT NULL                 | First name of the customer.          |
| customerLNAME     | VARCHAR(45)  | DEFAULT NULL                 | Last name of the customer.           |
| customerAddress   | VARCHAR(50)  | DEFAULT NULL                 | Address of the customer.             |
| customerProvince  | VARCHAR(50)  | DEFAULT NULL                 | Province of the customer.            |
| districtId        | INT          | FOREIGN KEY                  | District ID for the customer.        |
| subdistrictId     | INT          | FOREIGN KEY                  | Subdistrict ID for the customer.     |
| customerPostalCode| INT          | DEFAULT NULL                 | Postal code of the customer.         |
| customerPhone     | INT          | DEFAULT NULL                 | Phone number of the customer.        |
| customerImage     | VARCHAR(45)  | DEFAULT NULL                 | Image file name for the customer.    |


