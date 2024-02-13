# Graphql.Demo

*[Medium Article](https://omripk.medium.com/bir-%C3%B6nce-ki-makalemin-%C3%BCzerinden-uzunca-bir-zaman-ge%C3%A7ti-acac108079db)*.

# examples for queries;

## 1- get courses

```json
{
  courses{
  name
  id
}
}
```

## 2- getById

```json
{
  courseById(id: "a538ca60-e31f-4acc-a6cf-d3e1c5e41639"){
    name
    subject
    id
  }
}
```

## 3- filtering


parameter filtering.
```json
{
  courseByFiltering (
    where: { name: { contains: "Course" }}
  ){
    name
  }
}

```

---

# examples for mutation;

## 1- create course

```json
mutation{
  createCourse(name: "ömer", subject: HISTORY, instructorId: "a538ca60-e31f-4acc-a6cf-d3e1c5e41639")
{
  id
  name
  subject
}}
```

## 1.1- create course

```json
mutation {
createCourseV2(createOrUpdateCourseInputType: {
name: "Ömer",
instructorId: "dfece457-b98d-4876-83bc-1a3edb03c088",
subject: SCIENCE
}){
id
name
subject
}
}
```

## 2- update course

```json
mutation {
  updateCourse(
  id: "4425a4b7-7204-4a1d-a4cc-31c6e74a6840"
  name: "test"
  subject: MATHEMATICS,
  instructorId: "5425a4b7-7204-4a1d-a4cc-31c6e74a6843"
  ) {
  name
  id
  instructorId
  subject
}
}

```

## 3- delete course

```json
mutation{
  deleteCourse(id: "d3538480-1759-4b6b-9962-c5aba49d83d3")
}
```


---

## 1- subscription

## 1.1 - create subscription

```json

subscription{
courseCreated{
id
name
instructorId
}
}
```

## 1.2 - create course
```json
mutation {
  createCourseV2(
    createOrUpdateCourseInputType: {
      name: "Ömer"
      instructorId: "dfece457-b98d-4876-83bc-1a3edb03c088"
      subject: SCIENCE
    }
  ) {
    id
    name
    subject
  }
}

```

## 2.1 - update subscription

```json
subscription{
  courseUpdated(courseId: "d3538480-1759-4b6b-9962-c5aba49d83d3"){
    id
    name
    instructorId

  }
}
```

## 2.2 - update course

```json
mutation {
  updateCourse(
    id: "d3538480-1759-4b6b-9962-c5aba49d83d3"
    name: "test2"
    subject: MATHEMATICS
    instructorId: "5425a4b7-7204-4a1d-a4cc-31c6e74a6843"
  ) {
    name
    id
    instructorId
    subject
  }
}
```
