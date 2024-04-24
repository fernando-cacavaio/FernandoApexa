import { NavLink, useNavigate, useParams } from "react-router-dom";
import { ChangeEvent, useEffect, useState } from "react";
import apiConnector from "../../api/apiConnector.ts";
import { Button, Form, Segment } from "semantic-ui-react";
import { AdvisorDto } from "../../models/advisorDto.ts";

export default function MovieForm() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [advisor, setAdvisor] = useState<AdvisorDto>({
    id: undefined,
    name: "",
    address: "",
    healthStatus: "",
    phone: "",
    sin: "",
  });

  useEffect(() => {
    if (id) {
      apiConnector
        .getAdvisorById(Number(id))
        .then((advisor) => setAdvisor(advisor!));
    }
  }, [id]);

  function handleSubmitAdvisor() {
    if (!advisor.id) {
      apiConnector.createAdvisor(advisor).then(() => navigate("/"));
    } else {
      apiConnector.editAdvisor(advisor).then(() => navigate("/"));
    }
  }

  function handleInputChange(
    event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) {
    const { name, value } = event.target;
    setAdvisor({ ...advisor, [name]: value });
  }

  return (
    <Segment clearing>
      <Form
        onSubmit={handleSubmitAdvisor}
        autoComplete="off"
        className="ui invereted form"
      >
        <Form.Input
          label="Name"
          placeholder="Name"
          name="name"
          value={advisor.name}
          onChange={handleInputChange}
          maxLength="255"
          required={true}
        />
        <Form.Input
          label="SIN"
          placeholder="SIN"
          name="sin"
          value={advisor.sin}
          onChange={handleInputChange}
          onKeyPress={(event: any) => {
            if (!/[0-9]/.test(event.key)) {
              event.preventDefault();
            }
          }}
          maxLength="9"
          minLength="9"
          required={true}
        />
        <Form.Input
          label="Address"
          placeholder="Address"
          name="address"
          value={advisor.address}
          onChange={handleInputChange}
          maxLength="255"
        />
        <Form.Input
          label="Phone"
          placeholder="Phone"
          name="phone"
          value={advisor.phone}
          onChange={handleInputChange}
          onKeyPress={(event: any) => {
            if (!/[0-9]/.test(event.key)) {
              event.preventDefault();
            }
          }}
          maxLength="8"
          minLength="8"
        />
        <Button positive type="submit" content="Submit" />
        <Button as={NavLink} to="/" type="button" content="Cancel" />
      </Form>
    </Segment>
  );
}
