using sportex.api.domain;
using sportex.api.persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace sportex.api.logic
{
    public class RelationshipManager
    {
        #region PROPERTIES
        private IRepository<Relationship> repo;
        public RelationshipManager()
        {
            repo = new Repository<Relationship>();
        }
        #endregion

        #region BASIC CRUD

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
                return repo.SearchFor(r => r.IdProfile1 == id1 && r.IdProfile2 == id2).FirstOrDefault<Relationship>();
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

        private void UpdateRelationship(Relationship sends, Relationship receives)
        {
            try
            {
                if (sends != null)
                {
                    repo.Update(sends);
                }
                if (receives != null)
                {
                    repo.Update(receives);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region SEND FRIEND REQUEST

        //Sends friend request from a profile to another
        public string SendFriendRequest(int idProfileSends, int idProfileReceives)
        {
            try
            {
                IRepository<StandardProfile> profileRepo = new Repository<StandardProfile>();
                StandardProfile sends = profileRepo.GetById(idProfileSends);
                StandardProfile receives = profileRepo.GetById(idProfileReceives);
                if (sends != null && receives != null)
                {
                    return SendFriendRequest(sends, receives);
                }
                return "Los datos ingresados no son correctos";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Manages the output of a sent friend request
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
                    //Relationship newRequestSent = new Relationship(Relationship.RelationshipStatus.RequestSent, profileSends, profileReceives);
                    Relationship newRequestSent = new Relationship(Relationship.RelationshipStatus.RequestSent, profileSends.ID, profileReceives.ID);
                    //profileSends.Relationships.Add(newRequestSent);
                    //Relationship newRequestReceived = new Relationship(Relationship.RelationshipStatus.RequestReceived, profileReceives, profileSends);
                    Relationship newRequestReceived = new Relationship(Relationship.RelationshipStatus.RequestReceived, profileReceives.ID, profileSends.ID);
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

        #endregion
        

        #region ACCEPT FRIEND REQUEST

        //Accepts a friend request
        public string AcceptFriendRequest(int idProfileAccepts, int idProfileSent)
        {
            try
            {
                IRepository<StandardProfile> profileRepo = new Repository<StandardProfile>();
                StandardProfile sends = profileRepo.GetById(idProfileAccepts);
                StandardProfile receives = profileRepo.GetById(idProfileSent);
                if (sends != null && receives != null)
                {
                    return AcceptFriendRequest(sends, receives);
                }
                return "Los datos ingresados no son correctos";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Manages AcceptsFriendRequest
        public string AcceptFriendRequest(StandardProfile profileAccepts, StandardProfile profileSent)
        {
            try
            {
                Relationship relation = GetRelationship(profileAccepts.ID, profileSent.ID);
                if(relation.Status==(int)Relationship.RelationshipStatus.RequestReceived)
                {
                    relation.Status = (int)Relationship.RelationshipStatus.Friends;

                    Relationship relation2 = GetRelationship(profileSent.ID, profileAccepts.ID);
                    if (relation2 == null) throw new Exception();
                    else
                    {
                        relation2.Status = (int)Relationship.RelationshipStatus.Friends;
                        UpdateRelationship(relation, relation2);
                        return "Ahora eres amigo de " + profileSent.FirstName;
                    }
                }
                return "Error. No se ha podido aceptar la solicitud de amistad.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region OTHER METHODS

        //Gets all friend requests from a profile
        public List<StandardProfile> GetFriendRequests(int idProfile)
        {
            try
            {
                List<StandardProfile> requests = new List<StandardProfile>();
                List<Relationship> relationsRequested = repo.SearchFor(r => r.IdProfile1 == idProfile && r.Status == (int)Relationship.RelationshipStatus.RequestReceived).ToList<Relationship>();
                foreach (Relationship relation in relationsRequested)
                {
                    requests.Add(relation.Profile2);
                }
                return requests;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Sets a relationship to a state
        public void SetRelationshipStatus(StandardProfile profile1, StandardProfile profile2, Relationship.RelationshipStatus newStatus)
        {
            try
            {
                Relationship relation = GetRelationship(profile1.ID, profile2.ID);
                relation.Status = (int)newStatus;
                UpdateRelationship(relation, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
