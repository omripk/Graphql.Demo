import React from "react";
import axios from "axios";

const endpoint = "http://localhost:5292/graphql/";
const COURSES_QUERY = `
 {
    courses{
    name
    id
    subject
    }
}
`;

function GetGraphQLApi() {
  const [data, setter] = React.useState();
  const getMethod = () => {
    return axios({
      url: endpoint,
      method: "POST",
      data: {
        query: COURSES_QUERY
      }
    }).then(response => {
      setter(response.data.data);

    }
    );
  };

  React.useEffect(() => {
    getMethod();
  },
    [])

  if (!data) return "Loading...";

  return (
    <div>
      <h1>Courses</h1>
      <ul>
        {data.courses.map((course) => (

                    <li key={course.id}>{course.name} / {course.subject}</li>
                ))}
      </ul>
    </div>
  )
}

export default function App() {
  return <GetGraphQLApi></GetGraphQLApi>
}