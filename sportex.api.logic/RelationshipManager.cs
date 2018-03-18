using sportex.api.domain;
using sportex.api.persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace sportex.api.logic
{
    public class RelationshipManager
    {
        private IRepository<Relationship> repo;
        public RelationshipManager()
        {
            repo = new Repository<Relationship>();
        }
        //public List<Relationship> GetAllRelationships()
        //{
        //    try
        //    {
        //        List<Relationship> relationships = new List<Relationship>();
        //        relationships = repo.GetAll();
        //        return relationships;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public Relationship GetRelationshipById(int id)
        {
            try
            {
                return repo.GetById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertRelationship(Relationship relationship)
        {
            try
            {
                relationship.CreatedOn = DateTime.Now;
                relationship.LastUpdate = relationship.CreatedOn;
                repo.Insert(relationship);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Relationship GetRelationship(int id1, int id2)
        {
            try
            {
                return (Relationship)repo.SearchFor(r => r.IdProfile1 == id1 && r.IdProfile2 == id2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public Relationship GetRelationship(int id1, int id2)
        //{
        //    try
        //    {
        //        return (Relationship)repo.SearchFor(r => (r.IdProfile1 == id1 && r.IdProfile2 == id2) || (r.IdProfile1 == id2 && r.IdProfile2 == id1));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public string SendFriendRequest(StandardProfile profileSends, StandardProfile profileReceives)
        {
            try
            {
                //Chequear primero si el que recibe ya tiene alguna relacion con el que envia
                Relationship relationSends = GetRelationship(profileSends.ID, profileReceives.ID);
                Relationship relationReceives = GetRelationship(profileReceives.ID, profileSends.ID);
                //Relationship relation = profileReceives.Relationships1.Find(f => f.Profile.ID == profileSends.ID);
                if (relationSends == null && relationReceives == null)
                {
                    //No hay ninguna solicitud, envia una nueva
                    Relationship newRequestSent = new Relationship(Relationship.RelationshipStatus.RequestSent, profileSends, profileReceives);
                    //profileSends.Relationships.Add(newRequestSent);
                    Relationship newRequestReceived = new Relationship(Relationship.RelationshipStatus.RequestReceived, profileReceives, profileSends);
                    //profileReceives.Relationships.Add(newRequestReceived);
                    InsertRelationship(newRequestSent);
                    InsertRelationship(newRequestReceived);
                    return "Se ha enviado la solicitud de amistad a " + profileReceives.FirstName;
                }
                else
                {
                    if (relationSends != null)
                    {
                        switch (relationSends.Status)
                        {
                            case (int)Relationship.RelationshipStatus.Friends: //Ya son amigos
                                return "Ya eres amigo de " + profileReceives.FirstName;

                            case (int)Relationship.RelationshipStatus.RequestSent: //Ya le fue enviada una solicitud
                                return "Ya le has enviado una solicitud a " + profileReceives.FirstName;

                            case (int)Relationship.RelationshipStatus.RequestDeclined: //Lo habia rechazado, cambia a enviada
                                relationSends.Status = (int)Relationship.RelationshipStatus.RequestSent;
                                if (relationReceives == null)
                                {
                                    relationReceives = new Relationship((int)Relationship.RelationshipStatus.RequestReceived, profileReceives, profileSends);
                                    InsertRelationship(relationReceives);
                                    UpdateRelationship(relationSends, null);
                                    return "Se ha enviado la solicitud de amistad a " + profileReceives.FirstName;
                                }
                                break;

                            case (int)Relationship.RelationshipStatus.Blocked: //Lo habia bloqueado, cambia a enviada
                                relationSends.Status = (int)Relationship.RelationshipStatus.RequestSent;
                                if (relationReceives == null)
                                {
                                    relationReceives = new Relationship((int)Relationship.RelationshipStatus.RequestReceived, profileReceives, profileSends);
                                    InsertRelationship(relationReceives);
                                    UpdateRelationship(relationSends, null);
                                    return "Se ha enviado la solicitud de amistad a " + profileReceives.FirstName;
                                }
                                break;

                            case (int)Relationship.RelationshipStatus.RequestReceived: //Quien envia ya tenia una solicitud, genera amistad
                                relationSends.Status = (int)Relationship.RelationshipStatus.Friends;
                                if (relationReceives == null)
                                {
                                    relationReceives = new Relationship((int)Relationship.RelationshipStatus.Friends, profileReceives, profileSends);
                                    InsertRelationship(relationReceives);
                                    UpdateRelationship(relationSends, null);
                                }
                                else
                                {
                                    relationReceives.Status = (int)Relationship.RelationshipStatus.Friends;
                                    UpdateRelationship(relationSends, relationReceives);
                                }
                                return "Ahora eres amigo de " + profileReceives.FirstName;

                            default:
                                throw new Exception();
                        }
                    }
                    else
                    {
                        //Nunca habia enviado una solicitud, genera nueva
                        relationSends = new Relationship((int)Relationship.RelationshipStatus.RequestSent, profileSends, profileReceives);
                        InsertRelationship(relationSends);
                    }

                    if(relationReceives!=null)
                    {
                        switch (relationReceives.Status)
                        {
                            
                            case (int)Relationship.RelationshipStatus.RequestSent: //Quien recibe ya habia enviado solicitud, genera amistad (EN TEORIA NO DEBERIA ENTRAR NUNCA ACA)
                                relationSends.Status = (int)Relationship.RelationshipStatus.Friends;
                                relationReceives.Status = (int)Relationship.RelationshipStatus.Friends;
                                UpdateRelationship(relationSends, relationReceives); 
                                return "Ahora eres amigo de " + profileReceives.FirstName;

                            case (int)Relationship.RelationshipStatus.RequestDeclined: //Lo habia rechazado, sigue rechazando
                                UpdateRelationship(relationSends, null);
                                return profileReceives.FirstName + " ha rechazado su solicitud de amistad";

                            case (int)Relationship.RelationshipStatus.Blocked: //Lo habia bloqueado, sigue bloqueado
                                UpdateRelationship(relationSends, null);
                                return profileReceives.FirstName + " lo ha bloqueado";

                            case (int)Relationship.RelationshipStatus.RequestReceived: //Ya tenia una solicitud de amistad, sigue igual (TAMPOCO DEBERIA ENTRAR ACA)
                                UpdateRelationship(relationSends, null);
                                return "Ya le has enviado una solicitud a " + profileReceives.FirstName;

                            default:
                                throw new Exception();
                        }
                    }
                    else
                    {
                        //Nunca habia recibido una solicitud, genera nueva
                        relationReceives = new Relationship((int)Relationship.RelationshipStatus.RequestReceived, profileReceives, profileSends);
                        InsertRelationship(relationReceives);
                        UpdateRelationship(relationSends, null);
                        return "Se ha enviado la solicitud de amistad a " + profileReceives.FirstName;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void UpdateRelationship(Relationship sends, Relationship receives)
        {
            //actualizar en repo
        }


    }
}
