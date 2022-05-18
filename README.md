# Template endpoints

1. Get All (HttpGet request) Templates https://vap-microservices.herokuapp.com/api/Templates
   The received payload will look like :
   `[ { "name": "Test Template 2", "filters": [ { "requirement": "lorem ipsum 2", "operator": "lorem ipsum 2", "value": "lorem ipsum 2", "id": 18 }, { "requirement": "lorem ipsum 3", "operator": "lorem ipsum 3", "value": "lorem ipsum 3", "id": 19 } ], "id": 5 } ]`
2. Delete a Template (HttpPost request) (not in bulk) https://vap-microservices.herokuapp.com/api/Templates/delete/id
   The result is a bolean value indicating whether everything has been successfull or not
3. Add a Template (HttpPost request) https://vap-microservices.herokuapp.com/api/Templates
   The sent json has to look like as following:
   ` { "name": "Test Template 2", "filters": [ { "requirement": "lorem ipsum 2", "operator": "lorem ipsum 2", "value": "lorem ipsum" }, { "requirement": "lorem ipsum 3", "operator": "lorem ipsum 3", "value": "lorem ipsum" } ], }`
   The result is a bolean value indicating whether everything has been successfull or not.
   4 Update a Template (HttpPost request) https://vap-microservices.herokuapp.com/api/Templates/update
   The sent json has to look like as following (not in bulk):
   ` { "name": "Test Template 2", "id":2, "filters": [ { "requirement": "lorem ipsum 2", "operator": "lorem ipsum 2", "value": "lorem ipsum 2", "id": 18 }, { "requirement": "lorem ipsum 3", "operator": "lorem ipsum 3", "value": "lorem ipsum 3", } ] }`
   The result is a bolean value indicating whether everything has been successfull or not. Note that if you exclude id of any filter then a new filter will be generated and however many filters you pass to the method, those many filters will be created and old ones that you do not include are deleted automagically. So If, for example, a filter is deleted then you just don't include it in the list that you send to this endpoint.

# Assingment endpoints

Statuses:

- 0 - Assign
- 1 - Waitlist
- 2 - Free

1. Assign Or Waitlist (HttpPost request) (can be in bulk) https://vap-microservices.herokuapp.com/api/Assignments/AssignOrWaitlist
   The sent json has to look like as following:
   `[ { "id": 1, "role_offer_id":3 "status": 2 }, { "id": 2, "role_offer_id":3 "status": 0 } ] `
   The result is a bolean value indicating whether everything has been successfull or not.
2. Change to Any Status (HttpPost request) (can be in bulk) https://vap-microservices.herokuapp.com/api/Assignments/ChangeToAnyStatus
   The sent json has to look like as following:
   `[ { "id": 1, "status": 2 }, { "id": 2, "status": 0 } ] `
   The result is a bolean value indicating whether everything has been successfull or not.

# Role Offers endpoints

1. Get All (HttpGet request) https://vap-microservices.herokuapp.com/api/RoleOffers
   The received payload will look like :
   ` [ { "venue": { "name": "changedd", "code": null, "id": 15458 }, "functionalArea": { "name": "hahahahaha", "code": null, "id": 15974 }, "location": { "name": "Q22", "code": null, "id": 15458 }, "jobTitle": { "name": "Access Volunteer", "code": null, "id": 15943 }, "id": 12884 }`
2. Get By Id (HttpGet request) https://vap-microservices.herokuapp.com/api/RoleOffers/id
   The received payload will look like :
   ` { "venue": { "name": "Stadium 974", "code": null, "id": 15459 }, "functionalArea": { "name": "lalala", "code": null, "id": 15975 }, "location": { "name": "Q22", "code": null, "id": 15459 }, "jobTitle": { "name": "Access Volunteer", "code": null, "id": 15944 }, "id": 12885 }`
3. Import Role Offer (HttpPost request) https://vap-microservices.herokuapp.com/api/RoleOffers/import
   The method catches only files and the name has to be "file". The result is a bolean value indicating whether everything has been successfull or not.
