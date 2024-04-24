import { useEffect, useState } from "react";
import { AdvisorDto } from "../../models/advisorDto";
import apiConnector from "../../api/apiConnector";
import { Button, Container } from "semantic-ui-react";
import AdvisorTableItem from "./AdvisorTableItem";
import { NavLink } from "react-router-dom";

export default function AdvisorTable() {
  const [advisors, setAdvisors] = useState<AdvisorDto[]>([]);
  useEffect(() => {
    const fetchData = async () => {
      const fetchedAdvisors = await apiConnector.getAdvisors();
      setAdvisors(fetchedAdvisors);
    };

    fetchData();
  }, []);
  return (
    <>
      <Container className="container-style">
        <table className="ui table">
          <thead style={{ textAlign: "center" }}>
            <tr>
              <th>Name</th>
              <th>SIN</th>
              <th>Address</th>
              <th>Phone</th>
              <th>Health Status</th>
              <th>Action</th>
            </tr>
          </thead>
          <tbody>
            {advisors.length != 0 &&
              advisors.map((advisor, index) => (
                <AdvisorTableItem
                  key={index}
                  advisor={advisor}
                ></AdvisorTableItem>
              ))}
          </tbody>
        </table>
        <Button
          as={NavLink}
          to="createAdvisor"
          floated="right"
          type="button"
          content="Create Advisor"
          positive
        />
      </Container>
    </>
  );
}
