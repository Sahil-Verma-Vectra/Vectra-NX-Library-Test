using System;
using System.Collections.Generic;
using System.Linq;
using NXOpen.UF;
using NXOpen;
using Vectra.NX.ThreeD;
using Vectra.NX;
using NXOpen.Assemblies;

namespace Vectra_NX_Library_Testing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Session session = NXSupport.NXSession;
            Part workPart = NXSupport.WorkPart;
            VctPart vctPart = new VctPart(workPart);

            ListingWindow lw = session.ListingWindow;
            lw.Open();
            lw.WriteLine("==========================");

            List<Body> Bodyobj = new List<Body>();
            
            Component[] Components = vctPart.Part.ComponentAssembly.RootComponent.GetChildren();
            List<VctComponent> vctComponents = new List<VctComponent>();
            List<VctBody> vctBodies = new List<VctBody>();
            List<VctFace> vctFaces = new List<VctFace>();
            //lw.WriteLine(workPart.OwningComponent.JournalIdentifier);
            //lw.WriteLine(vctPart.Part.OwningComponent.JournalIdentifier);
            lw.WriteLine(Components[0].OwningComponent.JournalIdentifier);
            lw.WriteLine(new VctComponent(Components[0]).OwningComponent().JournalIdentifier);
            foreach (Component Component in Components)
            {
                vctComponents.Add(new VctComponent(Component));
                VctPart part = new VctPart((Part)Component.Prototype);
                part.LoadPartFully();
                
                foreach (Body body in part.Bodies.ToArray())
                {
                    Body objOccurrencebody = (Body)Component.FindOccurrence(body);
                    if (objOccurrencebody != null)
                    {
                        vctBodies.Add(new VctBody(objOccurrencebody));
                        Bodyobj.Add(objOccurrencebody);

                        foreach (Face face in body.GetFaces())
                        {
                            Face objFace = (Face)Component.FindOccurrence(face);
                            vctFaces.Add(new VctFace(objFace));
                        }
                    }
                }
            }

            //foreach (VctBody vctBody in vctBodies)
            //{
            //    vctBody.AllFacesUsingDirRayTracing(new double[3] { 0, 0, 0 }, new double[3] { 0, 1, 0 }, out VctFace[] faces);
            //    foreach (VctFace face1 in faces)
            //    {
            //        lw.WriteLine(face1.JournalIdentifier);
            //    }
            //}

            //for (int i = 0; i < vctBodies.Count; i++)
            //{
            //    List<KeyValuePair<VctFace, VctFace>> pairs = vctBodies[3].AllMatingFaces(vctPart, vctBodies[i]);
            //    foreach (KeyValuePair<VctFace, VctFace> pair in pairs)
            //    {
            //        lw.WriteLine(pair.Key.JournalIdentifier);
            //        lw.WriteLine(pair.Value.JournalIdentifier);
            //    }
            //    lw.WriteLine(pairs.Count.ToString());
            //    lw.WriteLine("----------------------------------------------------------------");
            //}

            //foreach (Body body in Bodyobj)
            //{
            //    vctBodies.Add(new VctBody(body));
            //    lw.WriteLine(body.JournalIdentifier);
            //    lw.WriteLine(body.IsSolidBody.ToString() + " ----> " + "Solid Body");
            //    lw.WriteLine(body.IsSheetBody.ToString() + " ----> " + "Sheet Body");
            //    Component comp = body.OwningComponent;
            //    lw.WriteLine(comp.DisplayName.ToString());
            //}

            //for (int i = 2; i < 15; i++)
            //{
            //    vctBodies.Add(new VctBody(Bodyobj[i]));
            //    lw.WriteLine(Bodyobj[i].JournalIdentifier);
            //    lw.WriteLine(Bodyobj[i].IsSolidBody.ToString() + "---->" + "Solid Body");
            //    lw.WriteLine(Bodyobj[i].IsSheetBody.ToString() + "---->" + "Sheet Body");
            //    Component comp = Bodyobj[i].OwningComponent;
            //    lw.WriteLine(comp.DisplayName.ToString());
            //}
            //List<VctBody> vctBodies1 = new List<VctBody>();
            //vctBodies1.Add(new VctBody(Body[1]));

            //List<VctBody> vctBodies2 = new List<VctBody>();
            //vctBodies1.Add(new VctBody(Body[3]));
            //foreach (Body body in Body)
            //{
            //    lw.WriteLine(body.NameLocation.X.ToString());
            //    lw.WriteLine(body.NameLocation.Y.ToString());
            //    lw.WriteLine(body.NameLocation.Z.ToString());
            //    lw.WriteLine("------------------------------------");
            //}

            //try
            //{
            //    lw.WriteLine(vctFaces[1].JournalIdentifier);
            //    lw.WriteLine(vctFaces[1].Color.ToString());
            //    vctFaces[1].Color = 66;
            //    vctFaces[1].face.RedisplayObject();
            //    VctFace face2 = new VctFace(vctFaces[1].face);
            //    lw.WriteLine(face2.Color.ToString() + " == VctFace Color");
            //    lw.WriteLine(vctFaces[1].face.Color.ToString() + " == Face Color");
            //}
            //catch (NXException ex)
            //{
            //    UI.GetUI().NXMessageBox.Show("Error", NXMessageBox.DialogType.Error, ex.ToString());
            //}
        }

        public static int GetUnloadOption(string dummy)
        {
            return (int)Session.LibraryUnloadOption.Immediately;
        }

        //public static List<Tuple<VctBody, VctBody, double, string>> DoClearanceAnalysisForBodies3(List<Body> RefBodies, List<Body> AssemblyComponents, string ClearanceThreshold)
        //{
        //    List<Tuple<VctBody, VctBody, double, string>> InterferenceComponents = new List<Tuple<VctBody, VctBody, double, string>>();
        //    ClearanceSet ClearanceSet = null;
        //    ClearanceAnalysisBuilder clearanceAnalysisBuilder = null;
        //    Expression expression = null;
        //    Unit unit = null;
        //    bool added;
        //    NXObject nXObject = null;
        //    clearanceAnalysisBuilder = NXSupport.NXSession.Parts.Work.AssemblyManager.CreateClearanceAnalysisBuilder(ClearanceSet);
        //    expression = clearanceAnalysisBuilder.CreateClearanceZoneExpression(ClearanceThreshold);
        //    unit = NXSupport.NXSession.Parts.Work.UnitCollection.FindObject("MilliMeter");
        //    expression.Units = unit;
        //    // Setting up the Clearance Analysis
        //    clearanceAnalysisBuilder.SetDefaultClearanceZone(expression);
        //    clearanceAnalysisBuilder.CalculationMethod = ClearanceAnalysisBuilder.CalculationMethodType.Exact;
        //    clearanceAnalysisBuilder.ClearanceSetName = "VectraSet";
        //    clearanceAnalysisBuilder.TotalCollectionCount = ClearanceAnalysisBuilder.NumberOfCollections.Two;
        //    clearanceAnalysisBuilder.CollectionOneRange = ClearanceAnalysisBuilder.CollectionRange.SelectedObjects;
        //    clearanceAnalysisBuilder.CollectionTwoRange = ClearanceAnalysisBuilder.CollectionRange.SelectedObjects;
        //    clearanceAnalysisBuilder.ClearanceBetween = ClearanceAnalysisBuilder.ClearanceBetweenEntity.Bodies;
        //    // Adding assembly files to collectionOneObjects
        //    foreach (Body body in AssemblyComponents)
        //    {
        //        if (!body.IsBlanked)
        //        {
        //            int status = NXSupport.UfSession.Obj.AskStatus(body.Tag);
        //            if (status == UFConstants.UF_OBJ_ALIVE)
        //            {
        //                added = clearanceAnalysisBuilder.CollectionOneObjects.Add(body);
        //            }
        //        }
        //    }

        //    foreach (Body body in RefBodies)
        //    {
        //        if (!body.IsBlanked)
        //        {
        //            int status = NXSupport.UfSession.Obj.AskStatus(body.Tag);
        //            if (status == UFConstants.UF_OBJ_ALIVE)
        //            {
        //                added = clearanceAnalysisBuilder.CollectionTwoObjects.Add(body);
        //            }
        //        }
        //    }

        //    nXObject = clearanceAnalysisBuilder.Commit();
        //    clearanceAnalysisBuilder.Destroy();
        //    ClearanceSet = (ClearanceSet)nXObject;

        //    try
        //    {
        //        // One or more objects were unavailable (unloaded) during clearance analysis
        //        ClearanceSet.PerformAnalysis(NXOpen.Assemblies.ClearanceSet.ReanalyzeOutOfDateExcludedPairs.False);


        //        // Analyze the clearance
        //        InterferenceComponents = InterferenceComponents3(ClearanceSet);
        //        ClearanceSet.Delete();
        //    }
        //    catch (NXException ex)
        //    {
        //        ex.AssertErrorCode(1185014);
        //    }
        //    return InterferenceComponents;
        //}

        //public static List<Tuple<VctBody, VctBody, double, string>> InterferenceComponents3(ClearanceSet clearanceSet)
        //{
        //    List<Tuple<VctBody, VctBody, double, string>> interferenceComponents = new List<Tuple<VctBody, VctBody, double, string>>();
        //    List<VctBody> selComponentList = new List<VctBody>();
        //    DisplayableObject FirstDispObj = null;
        //    DisplayableObject SecondDispObj = null;
        //    // Can be soft, touch, hard and no interference
        //    ClearanceSet.InterferenceType interferenceType;
        //    bool newInter;
        //    // List of interference bodies
        //    DisplayableObject[] interfBodies;
        //    // A point on the first object
        //    Point3d point3d1;
        //    // A point on the second object
        //    Point3d point3d2;
        //    // Text associated with the interference
        //    string text1 = string.Empty;
        //    // A unique number for the interference
        //    int interNum;
        //    // The configuration index
        //    int config1;
        //    // Result status of penetration depth calculation
        //    int depthResult;
        //    // A depth penetration
        //    double depth1;
        //    // A unit vector indicating the direction of penetration
        //    Vector3d dir1;
        //    // The points on the interference region at the extremes of depth
        //    Point3d minPoint;
        //    // The points on the interference region at the extremes of depth
        //    Point3d maxPoint;
        //    int numberOFInterferences = clearanceSet.GetNumberOfInterferences();
        //    if (numberOFInterferences > 0)
        //    {
        //        // FirstDispObj - the first object in the pair of the current interference
        //        // SecondDispObj - the second object in the pair of the current interference
        //        // NextFirstDispObj - the first object in the pair of the next interference
        //        // the second object in the pair of the next interference 
        //        do
        //        {
        //            DisplayableObject nextSecondDispObj;
        //            DisplayableObject nextFirstDispObj;
        //            // Interferences are stored as object pairs.
        //            // Start the cycling using a null reference (Nothing in Visual Basic) as the input values for both object1 and object2.
        //            // This routine passes back the tags of the objects that define the next interference.
        //            // A null reference (Nothing in Visual Basic) returned for either object indicates the end of the cycling.
        //            clearanceSet.GetNextInterference(FirstDispObj, SecondDispObj, out nextFirstDispObj, out nextSecondDispObj);
        //            FirstDispObj = nextFirstDispObj;
        //            SecondDispObj = nextSecondDispObj;
        //            if (nextFirstDispObj != null && nextSecondDispObj != null)
        //            {
        //                if (!selComponentList.Contains(new VctBody((Body)nextFirstDispObj)))
        //                {
        //                    selComponentList.Add(new VctBody((Body)nextFirstDispObj));
        //                }
        //                clearanceSet.GetInterferenceData(nextFirstDispObj, nextSecondDispObj, out interferenceType, out newInter, out interfBodies, out point3d1, out point3d2, out text1, out interNum, out config1, out depthResult, out depth1, out dir1, out minPoint, out maxPoint);
        //                // Get the mapping/clash analysis between product and components
        //                if (interferenceType != ClearanceSet.InterferenceType.NoInterference)
        //                {
        //                    interferenceComponents.Add(new Tuple<VctBody, VctBody, double, string>(new VctBody((Body)nextFirstDispObj), new VctBody((Body)nextSecondDispObj), depth1, interferenceType.ToString()));
        //                }
        //            }
        //        } while (FirstDispObj != null && SecondDispObj != null);
        //    }
        //    return interferenceComponents;
        //}
    }
}
