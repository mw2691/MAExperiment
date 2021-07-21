#################################
#################################
#################################
#participant demographic data ALL (8female & 7 male)
#ages <- c(21,20,28,19,29,20,19,35,25,20,23,27,19,28,25)
#meanAges <- mean(ages)
#sdAges <- sd(ages)

#participant demographic data freunde Versuchsleiter (2 female & 2 male)
#agesFriends <- c(29,27,28,25)
#meanAgesFriends <- mean(agesFriends)
#sdAgesFriends <- sd(agesFriends)
#################################
#################################
#################################



setwd("/Users/markuswieland/Documents/MADataAnalysis")
library(stringr)
library(signal)
library(dplyr)
library(schoRsch)
library(ez)
#library(mousetrap)



butterworthFilter <- butter(2, 0.10)
annotatedFilesFolder <- "AnnotatedData"
slash <- "/"
participandIDSeq <- seq(1,15,1)
participantID <- 1
pathOnlyWithID <- paste(annotatedFilesFolder,slash, participantID, sep = "")
allResultFilesFromGivenParticipantID <- list.files(pathOnlyWithID)



# participant1 <- matrix(nrow = 11, ncol=16)
# participant2 <- matrix(nrow = 11, ncol=16)
# participant3 <- matrix(nrow = 11, ncol=16)
# participant4 <- matrix(nrow = 11, ncol=16)
# participant5 <- matrix(nrow = 11, ncol=16)
# participant6 <- matrix(nrow = 11, ncol=16)
# participant7 <- matrix(nrow = 11, ncol=16)
# participant8 <- matrix(nrow = 11, ncol=16)
# participant9 <- matrix(nrow = 11, ncol=16)
# participant10 <- matrix(nrow = 11, ncol=16)
# participant11 <- matrix(nrow = 11, ncol=16)
# participant12 <- matrix(nrow = 11, ncol=16)
# participant13 <- matrix(nrow = 11, ncol=16)
# participant14 <- matrix(nrow = 11, ncol=16)
# participant15 <- matrix(nrow = 11, ncol=16)
# participant16 <- matrix(nrow = 11, ncol=16)
# 
# matricesList <- list(participant1, participant2, participant3, participant4, participant5,
#                      participant6, participant7, participant8, participant9, participant10,
#                      participant11, participant12, participant13, participant14, participant15,
#                      participant16)

matricesListPeakVelos <- list()
matricesListHorizontalMovement <- list()

##################Calculate derivations
finiteDifferences <- function(x, y) {
  if (length(x) != length(y)) {
    stop('x and y vectors must have equal length')
  }
  n <- length(x)
  fdx <- vector(length = n)
  for (i in 2:n) {
    fdx[i-1] <- (y[i-1] - y[i]) / (x[i-1] - x[i])
  }
  
  fdx[n] <- (y[n] - y[n - 1]) / (x[n] - x[n - 1])
  
  return(fdx)
}
##################Calculate derivations

for (k in participandIDSeq) {
  if (k == 5) next
  participantID <- as.character(k)
  if(k < 10){
    participantID <- paste("0", as.character(k), sep = "")
  }
  pathOnlyWithID <- paste(annotatedFilesFolder,slash, participantID, sep = "")
  allResultFilesFromGivenParticipantID <- list.files(pathOnlyWithID)
  
  PeakVelocitySOA1PlaceLeft <- matrix(nrow = 11, ncol = 10)
  PeakVelocitySOA2PlaceLeft <- matrix(nrow = 11, ncol = 10)
  PeakVelocitySOA3PlaceLeft <- matrix(nrow = 11, ncol = 10)
  PeakVelocitySOA4PlaceLeft <- matrix(nrow = 11, ncol = 10)
  PeakVelocitySOA1PlaceRight <- matrix(nrow = 11, ncol = 10)
  PeakVelocitySOA2PlaceRight <- matrix(nrow = 11, ncol = 10)
  PeakVelocitySOA3PlaceRight <- matrix(nrow = 11, ncol = 10)
  PeakVelocitySOA4PlaceRight <- matrix(nrow = 11, ncol = 10)
  PeakVelocitySOA1PourRight <- matrix(nrow = 11, ncol = 10)
  PeakVelocitySOA2PourRight <- matrix(nrow = 11, ncol = 10)
  PeakVelocitySOA3PourRight <- matrix(nrow = 11, ncol = 10)
  PeakVelocitySOA4PourRight <- matrix(nrow = 11, ncol = 10)
  PeakVelocitySOA1PourLeft <- matrix(nrow = 11, ncol = 10)
  PeakVelocitySOA2PourLeft <- matrix(nrow = 11, ncol = 10)
  PeakVelocitySOA3PourLeft <- matrix(nrow = 11, ncol = 10)
  PeakVelocitySOA4PourLeft <- matrix(nrow = 11, ncol = 10)
  
  PalmXSOA1PlaceLeft <- matrix(nrow = 11, ncol = 10)
  PalmXSOA2PlaceLeft <- matrix(nrow = 11, ncol = 10)
  PalmXSOA3PlaceLeft <- matrix(nrow = 11, ncol = 10)
  PalmXSOA4PlaceLeft <- matrix(nrow = 11, ncol = 10)
  PalmXSOA1PlaceRight <- matrix(nrow = 11, ncol = 10)
  PalmXSOA2PlaceRight <- matrix(nrow = 11, ncol = 10)
  PalmXSOA3PlaceRight <- matrix(nrow = 11, ncol = 10)
  PalmXSOA4PlaceRight <- matrix(nrow = 11, ncol = 10)
  PalmXSOA1PourRight <- matrix(nrow = 11, ncol = 10)
  PalmXSOA2PourRight <- matrix(nrow = 11, ncol = 10)
  PalmXSOA3PourRight <- matrix(nrow = 11, ncol = 10)
  PalmXSOA4PourRight <- matrix(nrow = 11, ncol = 10)
  PalmXSOA1PourLeft <- matrix(nrow = 11, ncol = 10)
  PalmXSOA2PourLeft <- matrix(nrow = 11, ncol = 10)
  PalmXSOA3PourLeft <- matrix(nrow = 11, ncol = 10)
  PalmXSOA4PourLeft <- matrix(nrow = 11, ncol = 10)
  
  rowCounter1 <- 1
  rowCounter2 <- 1
  rowCounter3 <- 1
  rowCounter4 <- 1
  rowCounter5 <- 1
  rowCounter6 <- 1
  rowCounter7 <- 1
  rowCounter8 <- 1
  rowCounter9 <- 1
  rowCounter10 <- 1
  rowCounter11 <- 1
  rowCounter12 <- 1
  rowCounter13 <- 1
  rowCounter14 <- 1
  rowCounter15 <- 1
  rowCounter16 <- 1
  
  for (filename in allResultFilesFromGivenParticipantID) {
    currentFile <- paste(filename,"::::::",participantID, "::::::i:", k, sep = "")
    print(currentFile)
    fullPath <- paste(annotatedFilesFolder,slash, participantID, slash, filename, sep = "" )
    dataMA <- read.table(fullPath, header = TRUE)
    #split data
    thumbXYZ <-  str_split_fixed(dataMA$ThumbPosition, ":", 3)
    indexXYZ <-  str_split_fixed(dataMA$IndexPosition, ":", 3)
    palmXYZ <-  str_split_fixed(dataMA$PalmPosition, ":", 3)
    objectXYZ <-  str_split_fixed(dataMA$ObjectPosition, ":", 3)
    gazeXYZ <-  str_split_fixed(dataMA$GazePosition, ":", 3)
    #data as numeric
    dataMA$ThumbX <- as.numeric(thumbXYZ[,1])  
    dataMA$ThumbY <- as.numeric(thumbXYZ[,2])
    dataMA$ThumbZ <- as.numeric(thumbXYZ[,3])
    dataMA$IndexX <- as.numeric(indexXYZ[,1])  
    dataMA$IndexY <- as.numeric(indexXYZ[,2])
    dataMA$IndexZ <- as.numeric(indexXYZ[,3])
    dataMA$PalmX <- as.numeric(palmXYZ[,1])  
    dataMA$PalmY <- as.numeric(palmXYZ[,2])
    dataMA$PalmZ <- as.numeric(palmXYZ[,3])
    dataMA$ObjectX <- as.numeric(objectXYZ[,1])  
    dataMA$ObjectY <- as.numeric(objectXYZ[,2])
    dataMA$ObjectZ <- as.numeric(objectXYZ[,3])
    dataMA$GazeX <- as.numeric(gazeXYZ[,1])  
    dataMA$GazeY <- as.numeric(gazeXYZ[,2])
    dataMA$GazeZ <- as.numeric(gazeXYZ[,3])
    dataMA <- na.omit(dataMA)

    #butterworth filtering
    dataMA$ThumbX <- signal::filter(butterworthFilter, dataMA$ThumbX)
    dataMA$ThumbY <- signal::filter(butterworthFilter, dataMA$ThumbY)
    dataMA$ThumbZ <- signal::filter(butterworthFilter, dataMA$ThumbZ)
    dataMA$IndexX <- signal::filter(butterworthFilter, dataMA$IndexX)
    dataMA$IndexY <- signal::filter(butterworthFilter, dataMA$IndexY)
    dataMA$IndexZ <- signal::filter(butterworthFilter, dataMA$IndexZ)
    dataMA$PalmX <- signal::filter(butterworthFilter, dataMA$PalmX)
    dataMA$PalmY <- signal::filter(butterworthFilter, dataMA$PalmY)
    dataMA$PalmZ <- signal::filter(butterworthFilter, dataMA$PalmZ)
    dataMA$ObjectX <- signal::filter(butterworthFilter, dataMA$ObjectX)
    dataMA$ObjectY <- signal::filter(butterworthFilter, dataMA$ObjectY)
    dataMA$ObjectZ <- signal::filter(butterworthFilter, dataMA$ObjectZ)
    dataMA$GazeX <- signal::filter(butterworthFilter, dataMA$GazeX)
    dataMA$GazeY <- signal::filter(butterworthFilter, dataMA$GazeY)
    dataMA$GazeZ <- signal::filter(butterworthFilter, dataMA$GazeZ)
    
    #filterung nach ReachForBottleTrials (+- 5 Trials)
    allReachForBottleRowNumbers <- (1:nrow(dataMA))[dataMA[,6] == "ReachForBottle"]
    firstRowIndexWithReachForBottle <- allReachForBottleRowNumbers[1]
    lastRowIndexWithReachForBottle <- allReachForBottleRowNumbers[length(allReachForBottleRowNumbers)]
    DataAllReachForBottleTrials <- dataMA[(firstRowIndexWithReachForBottle-5):(lastRowIndexWithReachForBottle+5),]
    DataAllReachForBottleTrials <- na.omit(DataAllReachForBottleTrials)

    #calculate derivations with Palm Z 
    t <- seq(1, length(DataAllReachForBottleTrials$PalmZ), len = length(DataAllReachForBottleTrials$PalmZ))
    derivations <- finiteDifferences(t, DataAllReachForBottleTrials$PalmZ)
    DataAllReachForBottleTrials$PalmZVelo <- derivations
    #plot(t,DataAllReachForBottleTrials$PalmZVelo, main = filename)
    
    quantileTrialList <- c()
    for (i in seq(0,1,0.1)) {
      #print(quantile(DataAllReachForBottleTrials$PalmZ,i))
      quantileTrialList <- c(quantileTrialList, unname(quantile(DataAllReachForBottleTrials$PalmZ,i)))
    }
    matchedVelos <- c()
    matchedPositions <- c()

    
    
    if(str_detect(filename, "SOA1_Place_Left")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA1PlaceLeft[,rowCounter1] <- c(matchedVelos)
      PalmXSOA1PlaceLeft[,rowCounter1] <- c(matchedPositions)
      rowCounter1 <- rowCounter1 + 1
    }
    if(str_detect(filename, "SOA2_Place_Left")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA2PlaceLeft[,rowCounter2] <- c(matchedVelos)
      PalmXSOA2PlaceLeft[,rowCounter2] <- c(matchedPositions)
      rowCounter2 <- rowCounter2 + 1
    }
    if(str_detect(filename, "SOA3_Place_Left")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA3PlaceLeft[,rowCounter3] <- c(matchedVelos)
      PalmXSOA3PlaceLeft[,rowCounter3] <- c(matchedPositions)
      rowCounter3 <- rowCounter3 + 1
    }
    if(str_detect(filename, "SOA4_Place_Left")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA4PlaceLeft[,rowCounter4] <- c(matchedVelos)
      PalmXSOA4PlaceLeft[,rowCounter4] <- c(matchedPositions)
      rowCounter4 <- rowCounter4 + 1
    }
    if(str_detect(filename, "SOA1_Place_Right")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA1PlaceRight[,rowCounter5] <- c(matchedVelos)
      PalmXSOA1PlaceRight[,rowCounter5] <- c(matchedPositions)
      rowCounter5 <- rowCounter5 + 1
    }
    if(str_detect(filename, "SOA2_Place_Right")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA2PlaceRight[,rowCounter6] <- c(matchedVelos)
      PalmXSOA2PlaceRight[,rowCounter6] <- c(matchedPositions)
      rowCounter6 <- rowCounter6 + 1
    }
    if(str_detect(filename, "SOA3_Place_Right")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA3PlaceRight[,rowCounter7] <- c(matchedVelos)
      PalmXSOA3PlaceRight[,rowCounter7] <- c(matchedPositions)
      rowCounter7 <- rowCounter7 + 1
    }
    if(str_detect(filename, "SOA4_Place_Right")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA4PlaceRight[,rowCounter8] <- c(matchedVelos)
      PalmXSOA4PlaceRight[,rowCounter8] <- c(matchedPositions)
      rowCounter8 <- rowCounter8 + 1
    }
    if(str_detect(filename, "SOA1_Pour_Right")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA1PourRight[,rowCounter9] <- c(matchedVelos)
      PalmXSOA1PourRight[,rowCounter9] <- c(matchedPositions)
      rowCounter9 <- rowCounter9 + 1
    }
    if(str_detect(filename, "SOA2_Pour_Right")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA2PourRight[,rowCounter10] <- c(matchedVelos)
      PalmXSOA2PourRight[,rowCounter10] <- c(matchedPositions)
      rowCounter10 <- rowCounter10 + 1
    }
    if(str_detect(filename, "SOA3_Pour_Right")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA3PourRight[,rowCounter11] <- c(matchedVelos)
      PalmXSOA3PourRight[,rowCounter11] <- c(matchedPositions)
      rowCounter11 <- rowCounter11 + 1
    }
    if(str_detect(filename, "SOA4_Pour_Right")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA4PourRight[,rowCounter12] <- c(matchedVelos)
      PalmXSOA4PourRight[,rowCounter12] <- c(matchedPositions)
      rowCounter12 <- rowCounter12 + 1
    }
    if(str_detect(filename, "SOA1_Pour_Left")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA1PourLeft[,rowCounter13] <- c(matchedVelos)
      PalmXSOA1PourLeft[,rowCounter13] <- c(matchedPositions)
      rowCounter13 <- rowCounter13 + 1
    }
    if(str_detect(filename, "SOA2_Pour_Left")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA2PourLeft[,rowCounter14] <- c(matchedVelos)
      PalmXSOA2PourLeft[,rowCounter14] <- c(matchedPositions)
      rowCounter14 <- rowCounter14 + 1
    }
    if(str_detect(filename, "SOA3_Pour_Left")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA3PourLeft[,rowCounter15] <- c(matchedVelos)
      PalmXSOA3PourLeft[,rowCounter15] <- c(matchedPositions)
      rowCounter15 <- rowCounter15 + 1
    }
    if(str_detect(filename, "SOA4_Pour_Left")){
      for (i in 1:length(quantileTrialList)){
        higherBorder <- quantileTrialList[i] * 1.03
        lowerBorder <- quantileTrialList[i] * 0.97
        DataAllReachForBottleTrials$Trues <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
        trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
        matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
        matchedPositions <- c(matchedPositions, DataAllReachForBottleTrials$PalmX[trueIndex[1]])
      }
      PeakVelocitySOA4PourLeft[,rowCounter16] <- c(matchedVelos)
      PalmXSOA4PourLeft[,rowCounter16] <- c(matchedPositions)
      rowCounter16 <- rowCounter16 + 1
    }
  }

  meanVeloSOA1PlaceR <- rowMeans(PeakVelocitySOA1PlaceRight)
  meanVeloSOA2PlaceR <- rowMeans(PeakVelocitySOA2PlaceRight)
  meanVeloSOA3PlaceR <- rowMeans(PeakVelocitySOA3PlaceRight)
  meanVeloSOA4PlaceR <- rowMeans(PeakVelocitySOA4PlaceRight)
  
  meanVeloSOA1PlaceL <- rowMeans(PeakVelocitySOA1PlaceLeft)
  meanVeloSOA2PlaceL <- rowMeans(PeakVelocitySOA2PlaceLeft)
  meanVeloSOA3PlaceL <- rowMeans(PeakVelocitySOA3PlaceLeft)
  meanVeloSOA4PlaceL <- rowMeans(PeakVelocitySOA4PlaceLeft)
  
  meanVeloSOA1PourL <- rowMeans(PeakVelocitySOA1PourLeft)
  meanVeloSOA2PourL <- rowMeans(PeakVelocitySOA2PourLeft)
  meanVeloSOA3PourL <- rowMeans(PeakVelocitySOA3PourLeft)
  meanVeloSOA4PourL <- rowMeans(PeakVelocitySOA4PourLeft)
  
  meanVeloSOA1PourR <- rowMeans(PeakVelocitySOA1PourRight)
  meanVeloSOA2PourR <- rowMeans(PeakVelocitySOA2PourRight)
  meanVeloSOA3PourR <- rowMeans(PeakVelocitySOA3PourRight)
  meanVeloSOA4PourR <- rowMeans(PeakVelocitySOA4PourRight)
  
  
  
  HoriMovSOA1PlaceR <- rowMeans(PalmXSOA1PlaceRight)
  HoriMovSOA2PlaceR <- rowMeans(PalmXSOA2PlaceRight)
  HoriMovSOA3PlaceR <- rowMeans(PalmXSOA3PlaceRight)
  HoriMovSOA4PlaceR <- rowMeans(PalmXSOA4PlaceRight)
  
  HoriMovSOA1PlaceL <- rowMeans(PalmXSOA1PlaceLeft)
  HoriMovSOA2PlaceL <- rowMeans(PalmXSOA2PlaceLeft)
  HoriMovSOA3PlaceL <- rowMeans(PalmXSOA3PlaceLeft)
  HoriMovSOA4PlaceL <- rowMeans(PalmXSOA4PlaceLeft)
  
  HoriMovSOA1PourL <- rowMeans(PalmXSOA1PourLeft)
  HoriMovSOA2PourL <- rowMeans(PalmXSOA2PourLeft)
  HoriMovSOA3PourL <- rowMeans(PalmXSOA3PourLeft)
  HoriMovSOA4PourL <- rowMeans(PalmXSOA4PourLeft)
  
  HoriMovSOA1PourR <- rowMeans(PalmXSOA1PourRight)
  HoriMovSOA2PourR <- rowMeans(PalmXSOA2PourRight)
  HoriMovSOA3PourR <- rowMeans(PalmXSOA3PourRight)
  HoriMovSOA4PourR <- rowMeans(PalmXSOA4PourRight)
  


  matricesListPeakVelos[[k]] <- cbind(meanVeloSOA1PlaceR, meanVeloSOA2PlaceR, meanVeloSOA3PlaceR, meanVeloSOA4PlaceR,
                                        meanVeloSOA1PlaceL, meanVeloSOA2PlaceL, meanVeloSOA3PlaceL, meanVeloSOA4PlaceL,
                                        meanVeloSOA1PourL, meanVeloSOA2PourL, meanVeloSOA3PourL, meanVeloSOA4PourL, 
                                        meanVeloSOA1PourR, meanVeloSOA2PourR, meanVeloSOA3PourR, meanVeloSOA4PourR)
  
  matricesListHorizontalMovement[[k]] <- cbind(HoriMovSOA1PlaceR, HoriMovSOA2PlaceR, HoriMovSOA3PlaceR, HoriMovSOA4PlaceR,
                                               HoriMovSOA1PlaceL, HoriMovSOA2PlaceL, HoriMovSOA3PlaceL, HoriMovSOA4PlaceL,
                                               HoriMovSOA1PourL, HoriMovSOA2PourL, HoriMovSOA3PourL, HoriMovSOA4PourL, 
                                               HoriMovSOA1PourR, HoriMovSOA2PourR, HoriMovSOA3PourR, HoriMovSOA1PourR)

  
  # if(k == 2){
  #   break
  # }
}

#####Peak Velos
SOA1PlaceR <- matrix(nrow = 11, ncol = 14)
SOA2PlaceR <- matrix(nrow = 11, ncol = 14)
SOA3PlaceR <- matrix(nrow = 11, ncol = 14)
SOA4PlaceR <- matrix(nrow = 11, ncol = 14)

SOA1PlaceL <- matrix(nrow = 11, ncol = 14)
SOA2PlaceL <- matrix(nrow = 11, ncol = 14)
SOA3PlaceL <- matrix(nrow = 11, ncol = 14)
SOA4PlaceL <- matrix(nrow = 11, ncol = 14)

SOA1PourL <- matrix(nrow = 11, ncol = 14)
SOA2PourL <- matrix(nrow = 11, ncol = 14)
SOA3PourL <- matrix(nrow = 11, ncol = 14)
SOA4PourL <- matrix(nrow = 11, ncol = 14)

SOA1PourR <- matrix(nrow = 11, ncol = 14)
SOA2PourR <- matrix(nrow = 11, ncol = 14)
SOA3PourR <- matrix(nrow = 11, ncol = 14)
SOA4PourR <- matrix(nrow = 11, ncol = 14)
#######


#####Horizontal PalmX movements
SOA1PlaceRHoriMov <- matrix(nrow = 11, ncol = 14)
SOA2PlaceRHoriMov <- matrix(nrow = 11, ncol = 14)
SOA3PlaceRHoriMov <- matrix(nrow = 11, ncol = 14)
SOA4PlaceRHoriMov <- matrix(nrow = 11, ncol = 14)

SOA1PlaceLHoriMov <- matrix(nrow = 11, ncol = 14)
SOA2PlaceLHoriMov <- matrix(nrow = 11, ncol = 14)
SOA3PlaceLHoriMov <- matrix(nrow = 11, ncol = 14)
SOA4PlaceLHoriMov <- matrix(nrow = 11, ncol = 14)

SOA1PourLHoriMov <- matrix(nrow = 11, ncol = 14)
SOA2PourLHoriMov <- matrix(nrow = 11, ncol = 14)
SOA3PourLHoriMov <- matrix(nrow = 11, ncol = 14)
SOA4PourLHoriMov <- matrix(nrow = 11, ncol = 14)

SOA1PourRHoriMov <- matrix(nrow = 11, ncol = 14)
SOA2PourRHoriMov <- matrix(nrow = 11, ncol = 14)
SOA3PourRHoriMov <- matrix(nrow = 11, ncol = 14)
SOA4PourRHoriMov <- matrix(nrow = 11, ncol = 14)
#####


aCounter <- 1
for (i in 1:length(matricesListPeakVelos)){
  if (i == 5) 
    next
  #print(paste("i: ", i, "counter: ", aCounter))
  SOA1PlaceR[,aCounter] <- matricesListPeakVelos[[i]][,1]
  SOA2PlaceR[,aCounter] <- matricesListPeakVelos[[i]][,2]
  SOA3PlaceR[,aCounter] <- matricesListPeakVelos[[i]][,3]
  SOA4PlaceR[,aCounter] <- matricesListPeakVelos[[i]][,4]
  
  SOA1PlaceL[,aCounter] <- matricesListPeakVelos[[i]][,5]
  SOA2PlaceL[,aCounter] <- matricesListPeakVelos[[i]][,6]
  SOA3PlaceL[,aCounter] <- matricesListPeakVelos[[i]][,7]
  SOA4PlaceL[,aCounter] <- matricesListPeakVelos[[i]][,8]
  
  SOA1PourL[,aCounter] <- matricesListPeakVelos[[i]][,9]
  SOA2PourL[,aCounter] <- matricesListPeakVelos[[i]][,10]
  SOA3PourL[,aCounter] <- matricesListPeakVelos[[i]][,11]
  SOA4PourL[,aCounter] <- matricesListPeakVelos[[i]][,12]
  
  SOA1PourR[,aCounter] <- matricesListPeakVelos[[i]][,13]
  SOA2PourR[,aCounter] <- matricesListPeakVelos[[i]][,14]
  SOA3PourR[,aCounter] <- matricesListPeakVelos[[i]][,15]
  SOA4PourR[,aCounter] <- matricesListPeakVelos[[i]][,16]
  
  
  
  SOA1PlaceRHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,1]
  SOA2PlaceRHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,2]
  SOA3PlaceRHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,3]
  SOA4PlaceRHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,4]
  
  SOA1PlaceLHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,5]
  SOA2PlaceLHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,6]
  SOA3PlaceLHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,7]
  SOA4PlaceLHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,8]
  
  SOA1PourLHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,9]
  SOA2PourLHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,10]
  SOA3PourLHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,11]
  SOA4PourLHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,12]
  
  SOA1PourRHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,13]
  SOA2PourRHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,14]
  SOA3PourRHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,15]
  SOA4PourRHoriMov[,aCounter] <- matricesListHorizontalMovement[[i]][,16]
  
  aCounter <- aCounter + 1
}

SOA1PlaceLResult <- cbind(c(mean(SOA1PlaceL[1,]), mean(SOA1PlaceL[2,]), mean(SOA1PlaceL[3,]), mean(SOA1PlaceL[4,]),
                          mean(SOA1PlaceL[5,]), mean(SOA1PlaceL[6,]), mean(SOA1PlaceL[7,]), mean(SOA1PlaceL[8,]),
                          mean(SOA1PlaceL[9,]), mean(SOA1PlaceL[10,]), mean(SOA1PlaceL[11,])), c(sd(SOA1PlaceL[1,]),
                          sd(SOA1PlaceL[2,]), sd(SOA1PlaceL[3,]), sd(SOA1PlaceL[4,]), sd(SOA1PlaceL[5,]), sd(SOA1PlaceL[6,]),
                          sd(SOA1PlaceL[7,]), sd(SOA1PlaceL[8,]), sd(SOA1PlaceL[9,]), sd(SOA1PlaceL[10,]), sd(SOA1PlaceL[11,])),
                          c(mean(SOA1PlaceLHoriMov[1,]), mean(SOA1PlaceLHoriMov[2,]), mean(SOA1PlaceLHoriMov[3,]),mean(SOA1PlaceLHoriMov[4,]),
                            mean(SOA1PlaceLHoriMov[5,]), mean(SOA1PlaceLHoriMov[6,]), mean(SOA1PlaceLHoriMov[7,]), mean(SOA1PlaceLHoriMov[8,]),
                            mean(SOA1PlaceLHoriMov[9,]), mean(SOA1PlaceLHoriMov[10,]), mean(SOA1PlaceLHoriMov[11,])), 
                          c(sd(SOA1PlaceLHoriMov[1,]),sd(SOA1PlaceLHoriMov[2,]), sd(SOA1PlaceLHoriMov[3,]), sd(SOA1PlaceLHoriMov[4,]), 
                            sd(SOA1PlaceLHoriMov[5,]), sd(SOA1PlaceLHoriMov[6,]), sd(SOA1PlaceLHoriMov[7,]), sd(SOA1PlaceLHoriMov[8,]),
                            sd(SOA1PlaceLHoriMov[9,]), sd(SOA1PlaceLHoriMov[10,]), sd(SOA1PlaceLHoriMov[11,])))
colnames(SOA1PlaceLResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")

SOA2PlaceLResult <- cbind(c(mean(SOA2PlaceL[1,]), mean(SOA2PlaceL[2,]), mean(SOA2PlaceL[3,]), mean(SOA2PlaceL[4,]),
                            mean(SOA2PlaceL[5,]), mean(SOA2PlaceL[6,]), mean(SOA2PlaceL[7,]), mean(SOA2PlaceL[8,]),
                            mean(SOA2PlaceL[9,]), mean(SOA2PlaceL[10,]), mean(SOA2PlaceL[11,])), 
                          c(sd(SOA2PlaceL[1,]),sd(SOA2PlaceL[2,]), sd(SOA2PlaceL[3,]), sd(SOA2PlaceL[4,]), 
                            sd(SOA2PlaceL[5,]), sd(SOA2PlaceL[6,]),sd(SOA2PlaceL[7,]), sd(SOA2PlaceL[8,]), 
                            sd(SOA2PlaceL[9,]), sd(SOA2PlaceL[10,]), sd(SOA2PlaceL[11,])),
                          c(mean(SOA2PlaceLHoriMov[1,]), mean(SOA2PlaceLHoriMov[2,]), mean(SOA2PlaceLHoriMov[3,]), mean(SOA2PlaceLHoriMov[4,]),
                            mean(SOA2PlaceLHoriMov[5,]), mean(SOA2PlaceLHoriMov[6,]), mean(SOA2PlaceLHoriMov[7,]), mean(SOA2PlaceLHoriMov[8,]),
                            mean(SOA2PlaceLHoriMov[9,]), mean(SOA2PlaceLHoriMov[10,]), mean(SOA2PlaceLHoriMov[11,])),
                          c(sd(SOA2PlaceLHoriMov[1,]), sd(SOA2PlaceLHoriMov[2,]), sd(SOA2PlaceLHoriMov[3,]), sd(SOA2PlaceLHoriMov[4,]),
                            sd(SOA2PlaceLHoriMov[5,]), sd(SOA2PlaceLHoriMov[6,]), sd(SOA2PlaceLHoriMov[7,]), sd(SOA2PlaceLHoriMov[8,]),
                            sd(SOA2PlaceLHoriMov[9,]), sd(SOA2PlaceLHoriMov[10,]), sd(SOA2PlaceLHoriMov[11,])))
colnames(SOA2PlaceLResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")

SOA3PlaceLResult <- cbind(c(mean(SOA3PlaceL[1,]), mean(SOA3PlaceL[2,]), mean(SOA3PlaceL[3,]), mean(SOA3PlaceL[4,]),
                            mean(SOA3PlaceL[5,]), mean(SOA3PlaceL[6,]), mean(SOA3PlaceL[7,]), mean(SOA3PlaceL[8,]),
                            mean(SOA3PlaceL[9,]), mean(SOA3PlaceL[10,]), mean(SOA3PlaceL[11,])), 
                          c(sd(SOA3PlaceL[1,]), sd(SOA3PlaceL[2,]), sd(SOA3PlaceL[3,]), sd(SOA3PlaceL[4,]), 
                            sd(SOA3PlaceL[5,]), sd(SOA3PlaceL[6,]),sd(SOA3PlaceL[7,]), sd(SOA3PlaceL[8,]), 
                            sd(SOA3PlaceL[9,]), sd(SOA3PlaceL[10,]), sd(SOA3PlaceL[11,])), 
                          c(mean(SOA3PlaceLHoriMov[1,]), mean(SOA3PlaceLHoriMov[2,]), mean(SOA3PlaceLHoriMov[3,]), mean(SOA3PlaceLHoriMov[4,]),
                            mean(SOA3PlaceLHoriMov[5,]), mean(SOA3PlaceLHoriMov[6,]), mean(SOA3PlaceLHoriMov[7,]), mean(SOA3PlaceLHoriMov[8,]),
                            mean(SOA3PlaceLHoriMov[9,]), mean(SOA3PlaceLHoriMov[10,]), mean(SOA3PlaceLHoriMov[11,])), 
                          c(sd(SOA3PlaceLHoriMov[1,]), sd(SOA3PlaceLHoriMov[2,]), sd(SOA3PlaceLHoriMov[3,]), sd(SOA3PlaceLHoriMov[4,]),
                            sd(SOA3PlaceLHoriMov[5,]), sd(SOA3PlaceLHoriMov[6,]), sd(SOA3PlaceLHoriMov[7,]), sd(SOA3PlaceLHoriMov[8,]),
                            sd(SOA3PlaceLHoriMov[9,]), sd(SOA3PlaceLHoriMov[10,]), sd(SOA3PlaceLHoriMov[11,])))
colnames(SOA3PlaceLResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")

SOA4PlaceLResult <- cbind(c(mean(SOA4PlaceL[1,]), mean(SOA4PlaceL[2,]), mean(SOA4PlaceL[3,]), mean(SOA4PlaceL[4,]),
                            mean(SOA4PlaceL[5,]), mean(SOA4PlaceL[6,]), mean(SOA4PlaceL[7,]), mean(SOA4PlaceL[8,]),
                            mean(SOA4PlaceL[9,]), mean(SOA4PlaceL[10,]), mean(SOA4PlaceL[11,])), 
                          c(sd(SOA4PlaceL[1,]), sd(SOA4PlaceL[2,]), sd(SOA4PlaceL[3,]), sd(SOA4PlaceL[4,]), 
                            sd(SOA4PlaceL[5,]), sd(SOA4PlaceL[6,]),sd(SOA4PlaceL[7,]), sd(SOA4PlaceL[8,]), 
                            sd(SOA4PlaceL[9,]), sd(SOA4PlaceL[10,]), sd(SOA4PlaceL[11,])), 
                          c(mean(SOA4PlaceLHoriMov[1,]), mean(SOA4PlaceLHoriMov[2,]), mean(SOA4PlaceLHoriMov[3,]), mean(SOA4PlaceLHoriMov[4,]),
                            mean(SOA4PlaceLHoriMov[5,]), mean(SOA4PlaceLHoriMov[6,]), mean(SOA4PlaceLHoriMov[7,]), mean(SOA4PlaceLHoriMov[8,]),
                            mean(SOA4PlaceLHoriMov[9,]), mean(SOA4PlaceLHoriMov[10,]), mean(SOA4PlaceLHoriMov[11,])), 
                          c(sd(SOA4PlaceLHoriMov[1,]), sd(SOA4PlaceLHoriMov[2,]), sd(SOA4PlaceLHoriMov[3,]), sd(SOA4PlaceLHoriMov[4,]),
                            sd(SOA4PlaceLHoriMov[5,]), sd(SOA4PlaceLHoriMov[6,]), sd(SOA4PlaceLHoriMov[7,]), sd(SOA4PlaceLHoriMov[8,]),
                            sd(SOA4PlaceLHoriMov[9,]), sd(SOA4PlaceLHoriMov[10,]), sd(SOA4PlaceLHoriMov[11,])))
colnames(SOA4PlaceLResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")





SOA1PlaceRResult <- cbind(c(mean(SOA1PlaceR[1,]), mean(SOA1PlaceR[2,]), mean(SOA1PlaceR[3,]), mean(SOA1PlaceR[4,]),
                            mean(SOA1PlaceR[5,]), mean(SOA1PlaceR[6,]), mean(SOA1PlaceR[7,]), mean(SOA1PlaceR[8,]),
                            mean(SOA1PlaceR[9,]), mean(SOA1PlaceR[10,]), mean(SOA1PlaceR[11,])), 
                          c(sd(SOA1PlaceR[1,]), sd(SOA1PlaceR[2,]), sd(SOA1PlaceR[3,]), sd(SOA1PlaceR[4,]), 
                            sd(SOA1PlaceR[5,]), sd(SOA1PlaceR[6,]), sd(SOA1PlaceR[7,]), sd(SOA1PlaceR[8,]), 
                            sd(SOA1PlaceR[9,]), sd(SOA1PlaceR[10,]), sd(SOA1PlaceR[11,])), 
                          c(mean(SOA1PlaceRHoriMov[1,]), mean(SOA1PlaceRHoriMov[2,]), mean(SOA1PlaceRHoriMov[3,]),mean(SOA1PlaceRHoriMov[4,]),
                            mean(SOA1PlaceRHoriMov[5,]), mean(SOA1PlaceRHoriMov[6,]), mean(SOA1PlaceRHoriMov[7,]),mean(SOA1PlaceRHoriMov[8,]),
                            mean(SOA1PlaceRHoriMov[9,]), mean(SOA1PlaceRHoriMov[10,]), mean(SOA1PlaceRHoriMov[11,])), 
                          c(sd(SOA1PlaceRHoriMov[1,]), sd(SOA1PlaceRHoriMov[2,]), sd(SOA1PlaceRHoriMov[3,]), sd(SOA1PlaceRHoriMov[4,]),
                            sd(SOA1PlaceRHoriMov[5,]), sd(SOA1PlaceRHoriMov[6,]), sd(SOA1PlaceRHoriMov[7,]), sd(SOA1PlaceRHoriMov[8,]),
                            sd(SOA1PlaceRHoriMov[9,]), sd(SOA1PlaceRHoriMov[10,]), sd(SOA1PlaceRHoriMov[11,])))
colnames(SOA1PlaceRResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")

SOA2PlaceRResult <- cbind(c(mean(SOA2PlaceR[1,]), mean(SOA2PlaceR[2,]), mean(SOA2PlaceR[3,]), mean(SOA2PlaceR[4,]),
                            mean(SOA2PlaceR[5,]), mean(SOA2PlaceR[6,]), mean(SOA2PlaceR[7,]), mean(SOA2PlaceR[8,]),
                            mean(SOA2PlaceR[9,]), mean(SOA2PlaceR[10,]), mean(SOA2PlaceR[11,])), 
                          c(sd(SOA2PlaceR[1,]),sd(SOA2PlaceR[2,]), sd(SOA2PlaceR[3,]), sd(SOA2PlaceR[4,]), 
                            sd(SOA2PlaceR[5,]), sd(SOA2PlaceR[6,]),sd(SOA2PlaceR[7,]), sd(SOA2PlaceR[8,]), 
                            sd(SOA2PlaceR[9,]), sd(SOA2PlaceR[10,]), sd(SOA2PlaceR[11,])), 
                          c(mean(SOA2PlaceRHoriMov[1,]), mean(SOA2PlaceRHoriMov[2,]), mean(SOA2PlaceRHoriMov[3,]), mean(SOA2PlaceRHoriMov[4,]),
                            mean(SOA2PlaceRHoriMov[5,]), mean(SOA2PlaceRHoriMov[6,]), mean(SOA2PlaceRHoriMov[7,]), mean(SOA2PlaceRHoriMov[8,]),
                            mean(SOA2PlaceRHoriMov[9,]), mean(SOA2PlaceRHoriMov[10]), mean(SOA2PlaceRHoriMov[11,])), 
                          c(sd(SOA2PlaceRHoriMov[1, ]), sd(SOA2PlaceRHoriMov[2, ]), sd(SOA2PlaceRHoriMov[3, ]), sd(SOA2PlaceRHoriMov[4, ]),
                            sd(SOA2PlaceRHoriMov[5, ]), sd(SOA2PlaceRHoriMov[6, ]), sd(SOA2PlaceRHoriMov[7, ]), sd(SOA2PlaceRHoriMov[8, ]),
                            sd(SOA2PlaceRHoriMov[9, ]), sd(SOA2PlaceRHoriMov[10, ]), sd(SOA2PlaceRHoriMov[11,])))
colnames(SOA2PlaceRResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")

SOA3PlaceRResult <- cbind(c(mean(SOA3PlaceR[1,]), mean(SOA3PlaceR[2,]), mean(SOA3PlaceR[3,]), mean(SOA3PlaceR[4,]),
                            mean(SOA3PlaceR[5,]), mean(SOA3PlaceR[6,]), mean(SOA3PlaceR[7,]), mean(SOA3PlaceR[8,]),
                            mean(SOA3PlaceR[9,]), mean(SOA3PlaceR[10,]), mean(SOA3PlaceR[11,])), 
                          c(sd(SOA3PlaceR[1,]), sd(SOA3PlaceR[2,]), sd(SOA3PlaceR[3,]), sd(SOA3PlaceR[4,]), 
                            sd(SOA3PlaceR[5,]), sd(SOA3PlaceR[6,]),sd(SOA3PlaceR[7,]), sd(SOA3PlaceR[8,]), 
                            sd(SOA3PlaceR[9,]), sd(SOA3PlaceR[10,]), sd(SOA3PlaceR[11,])), 
                          c(mean(SOA3PlaceRHoriMov[1,]),mean(SOA3PlaceRHoriMov[2,]), mean(SOA3PlaceRHoriMov[3,]), mean(SOA3PlaceRHoriMov[4,]),
                            mean(SOA3PlaceRHoriMov[5,]), mean(SOA3PlaceRHoriMov[6,]), mean(SOA3PlaceRHoriMov[7,]), mean(SOA3PlaceRHoriMov[8,]),
                            mean(SOA3PlaceRHoriMov[9,]), mean(SOA3PlaceRHoriMov[10,]), mean(SOA3PlaceRHoriMov[11,])), 
                          c(sd(SOA3PlaceRHoriMov[1,]), sd(SOA3PlaceRHoriMov[2,]), sd(SOA3PlaceRHoriMov[3,]), sd(SOA3PlaceRHoriMov[4,]),
                            sd(SOA3PlaceRHoriMov[5,]), sd(SOA3PlaceRHoriMov[6,]), sd(SOA3PlaceRHoriMov[7,]), sd(SOA3PlaceRHoriMov[8,]),
                            sd(SOA3PlaceRHoriMov[9,]), sd(SOA3PlaceRHoriMov[10,]), sd(SOA3PlaceRHoriMov[11,])))
colnames(SOA3PlaceRResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")

SOA4PlaceRResult <- cbind(c(mean(SOA4PlaceR[1,]), mean(SOA4PlaceR[2,]), mean(SOA4PlaceR[3,]), mean(SOA4PlaceR[4,]),
                            mean(SOA4PlaceR[5,]), mean(SOA4PlaceR[6,]), mean(SOA4PlaceR[7,]), mean(SOA4PlaceR[8,]),
                            mean(SOA4PlaceR[9,]), mean(SOA4PlaceR[10,]), mean(SOA4PlaceR[11,])), 
                          c(sd(SOA4PlaceR[1,]), sd(SOA4PlaceR[2,]), sd(SOA4PlaceR[3,]), sd(SOA4PlaceR[4,]), 
                            sd(SOA4PlaceR[5,]), sd(SOA4PlaceR[6,]), sd(SOA4PlaceR[7,]), sd(SOA4PlaceR[8,]), 
                            sd(SOA4PlaceR[9,]), sd(SOA4PlaceR[10,]), sd(SOA4PlaceR[11,])), 
                          c(mean(SOA4PlaceRHoriMov[1,]), mean(SOA4PlaceRHoriMov[2,]), mean(SOA4PlaceRHoriMov[3,]), mean(SOA4PlaceRHoriMov[4,]),
                            mean(SOA4PlaceRHoriMov[5,]), mean(SOA4PlaceRHoriMov[6,]), mean(SOA4PlaceRHoriMov[7,]), mean(SOA4PlaceRHoriMov[8,]),
                            mean(SOA4PlaceRHoriMov[9,]), mean(SOA4PlaceRHoriMov[10,]), mean(SOA4PlaceRHoriMov[11,])), 
                          c(sd(SOA4PlaceRHoriMov[1,]), sd(SOA4PlaceRHoriMov[2,]), sd(SOA4PlaceRHoriMov[3,]), sd(SOA4PlaceRHoriMov[4,]),
                            sd(SOA4PlaceRHoriMov[5,]), sd(SOA4PlaceRHoriMov[6,]), sd(SOA4PlaceRHoriMov[7,]), sd(SOA4PlaceRHoriMov[8,]),
                            sd(SOA4PlaceRHoriMov[9,]), sd(SOA4PlaceRHoriMov[10,]), sd(SOA4PlaceRHoriMov[11,])))
colnames(SOA4PlaceRResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")




SOA1PourRResult <- cbind(c(mean(SOA1PourR[1,]), mean(SOA1PourR[2,]), mean(SOA1PourR[3,]), mean(SOA1PourR[4,]),
                            mean(SOA1PourR[5,]), mean(SOA1PourR[6,]), mean(SOA1PourR[7,]), mean(SOA1PourR[8,]),
                            mean(SOA1PourR[9,]), mean(SOA1PourR[10,]), mean(SOA1PourR[11,])), 
                         c(sd(SOA1PourR[1,]), sd(SOA1PourR[2,]), sd(SOA1PourR[3,]), sd(SOA1PourR[4,]), 
                           sd(SOA1PourR[5,]), sd(SOA1PourR[6,]), sd(SOA1PourR[7,]), sd(SOA1PourR[8,]), 
                           sd(SOA1PourR[9,]), sd(SOA1PourR[10,]), sd(SOA1PourR[11,])), 
                         c(mean(SOA1PourRHoriMov[1,]), mean(SOA1PourRHoriMov[2,]), mean(SOA1PourRHoriMov[3,]), mean(SOA1PourRHoriMov[4,]),
                           mean(SOA1PourRHoriMov[5,]), mean(SOA1PourRHoriMov[6,]), mean(SOA1PourRHoriMov[7,]), mean(SOA1PourRHoriMov[8,]),
                           mean(SOA1PourRHoriMov[9,]), mean(SOA1PourRHoriMov[10,]), mean(SOA1PourRHoriMov[11,])), 
                         c(sd(SOA1PourRHoriMov[1,]), sd(SOA1PourRHoriMov[2,]), sd(SOA1PourRHoriMov[3,]), sd(SOA1PourRHoriMov[4,]),
                           sd(SOA1PourRHoriMov[5,]), sd(SOA1PourRHoriMov[6,]), sd(SOA1PourRHoriMov[7,]), sd(SOA1PourRHoriMov[8,]),
                           sd(SOA1PourRHoriMov[9,]), sd(SOA1PourRHoriMov[10,]), sd(SOA1PourRHoriMov[11,])))
colnames(SOA1PourRResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")

SOA2PourRResult <- cbind(c(mean(SOA2PourR[1,]), mean(SOA2PourR[2,]), mean(SOA2PourR[3,]), mean(SOA2PourR[4,]),
                           mean(SOA2PourR[5,]), mean(SOA2PourR[6,]), mean(SOA2PourR[7,]), mean(SOA2PourR[8,]),
                           mean(SOA2PourR[9,]), mean(SOA2PourR[10,]), mean(SOA2PourR[11,])), 
                         c(sd(SOA2PourR[1,]), sd(SOA2PourR[2,]), sd(SOA2PourR[3,]), sd(SOA2PourR[4,]), 
                           sd(SOA2PourR[5,]), sd(SOA2PourR[6,]), sd(SOA2PourR[7,]), sd(SOA2PourR[8,]), 
                           sd(SOA2PourR[9,]), sd(SOA2PourR[10,]), sd(SOA2PourR[11,])), 
                         c(mean(SOA2PourRHoriMov[1,]), mean(SOA2PourRHoriMov[2,]), mean(SOA2PourRHoriMov[3,]), mean(SOA2PourRHoriMov[4,]),
                           mean(SOA2PourRHoriMov[5,]), mean(SOA2PourRHoriMov[6,]), mean(SOA2PourRHoriMov[7,]), mean(SOA2PourRHoriMov[8,]),
                           mean(SOA2PourRHoriMov[9,]), mean(SOA2PourRHoriMov[10,]), mean(SOA2PourRHoriMov[11,])), 
                         c(sd(SOA2PourRHoriMov[1,]), sd(SOA2PourRHoriMov[2,]), sd(SOA2PourRHoriMov[3,]), sd(SOA2PourRHoriMov[4,]),
                           sd(SOA2PourRHoriMov[5,]), sd(SOA2PourRHoriMov[6,]), sd(SOA2PourRHoriMov[7,]), sd(SOA2PourRHoriMov[8,]),
                           sd(SOA2PourRHoriMov[9,]), sd(SOA2PourRHoriMov[10,]), sd(SOA2PourRHoriMov[11,])))
colnames(SOA2PourRResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")

SOA3PourRResult <- cbind(c(mean(SOA3PourR[1,]), mean(SOA3PourR[2,]), mean(SOA3PourR[3,]), mean(SOA3PourR[4,]),
                           mean(SOA3PourR[5,]), mean(SOA3PourR[6,]), mean(SOA3PourR[7,]), mean(SOA3PourR[8,]),
                           mean(SOA3PourR[9,]), mean(SOA3PourR[10,]), mean(SOA3PourR[11,])), 
                         c(sd(SOA3PourR[1,]), sd(SOA3PourR[2,]), sd(SOA3PourR[3,]), sd(SOA3PourR[4,]), 
                           sd(SOA3PourR[5,]), sd(SOA3PourR[6,]), sd(SOA3PourR[7,]), sd(SOA3PourR[8,]), 
                           sd(SOA3PourR[9,]), sd(SOA3PourR[10,]), sd(SOA3PourR[11,])), 
                         c(mean(SOA3PourRHoriMov[1,]), mean(SOA3PourRHoriMov[2,]), mean(SOA3PourRHoriMov[3,]), mean(SOA3PourRHoriMov[4,]),
                           mean(SOA3PourRHoriMov[5,]), mean(SOA3PourRHoriMov[6,]), mean(SOA3PourRHoriMov[7,]), mean(SOA3PourRHoriMov[8,]),
                           mean(SOA3PourRHoriMov[9,]), mean(SOA3PourRHoriMov[10,]), mean(SOA3PourRHoriMov[11,])), 
                         c(sd(SOA3PourRHoriMov[1,]), sd(SOA3PourRHoriMov[2,]), sd(SOA3PourRHoriMov[3,]), sd(SOA3PourRHoriMov[4,]),
                           sd(SOA3PourRHoriMov[5,]), sd(SOA3PourRHoriMov[6,]), sd(SOA3PourRHoriMov[7,]), sd(SOA3PourRHoriMov[8,]),
                           sd(SOA3PourRHoriMov[9,]), sd(SOA3PourRHoriMov[10,]), sd(SOA3PourRHoriMov[11,])))
colnames(SOA3PourRResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")

SOA4PourRResult <- cbind(c(mean(SOA4PourR[1,]), mean(SOA4PourR[2,]), mean(SOA4PourR[3,]), mean(SOA4PourR[4,]),
                           mean(SOA4PourR[5,]), mean(SOA4PourR[6,]), mean(SOA4PourR[7,]), mean(SOA4PourR[8,]),
                           mean(SOA4PourR[9,]), mean(SOA4PourR[10,]), mean(SOA4PourR[11,])), 
                         c(sd(SOA4PourR[1,]), sd(SOA4PourR[2,]), sd(SOA4PourR[3,]), sd(SOA4PourR[4,]), 
                           sd(SOA4PourR[5,]), sd(SOA4PourR[6,]), sd(SOA4PourR[7,]), sd(SOA4PourR[8,]), 
                           sd(SOA4PourR[9,]), sd(SOA4PourR[10,]), sd(SOA4PourR[11,])), 
                         c(mean(SOA4PourRHoriMov[1,]), mean(SOA4PourRHoriMov[2,]), mean(SOA4PourRHoriMov[3,]), mean(SOA4PourRHoriMov[4,]),
                           mean(SOA4PourRHoriMov[5,]), mean(SOA4PourRHoriMov[6,]), mean(SOA4PourRHoriMov[7,]), mean(SOA4PourRHoriMov[8,]),
                           mean(SOA4PourRHoriMov[9,]), mean(SOA4PourRHoriMov[10,]), mean(SOA4PourRHoriMov[11,])), 
                         c(sd(SOA4PourRHoriMov[1,]), sd(SOA4PourRHoriMov[2,]), sd(SOA4PourRHoriMov[3,]), sd(SOA4PourRHoriMov[4,]),
                           sd(SOA4PourRHoriMov[5,]), sd(SOA4PourRHoriMov[6,]), sd(SOA4PourRHoriMov[7,]), sd(SOA4PourRHoriMov[8,]),
                           sd(SOA4PourRHoriMov[9,]), sd(SOA4PourRHoriMov[10,]), sd(SOA4PourRHoriMov[11,])))
colnames(SOA4PourRResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")


SOA1PourLResult <- cbind(c(mean(SOA1PourL[1,]), mean(SOA1PourL[2,]), mean(SOA1PourL[3,]), mean(SOA1PourL[4,]),
                           mean(SOA1PourL[5,]), mean(SOA1PourL[6,]), mean(SOA1PourL[7,]), mean(SOA1PourL[8,]),
                           mean(SOA1PourL[9,]), mean(SOA1PourL[10,]), mean(SOA1PourL[11,])), 
                         c(sd(SOA1PourL[1,]), sd(SOA1PourL[2,]), sd(SOA1PourL[3,]), sd(SOA1PourL[4,]), 
                           sd(SOA1PourL[5,]), sd(SOA1PourL[6,]), sd(SOA1PourL[7,]), sd(SOA1PourL[8,]), 
                           sd(SOA1PourL[9,]), sd(SOA1PourL[10,]), sd(SOA1PourL[11,])), 
                         c(mean(SOA1PourLHoriMov[1,]), mean(SOA1PourLHoriMov[2,]), mean(SOA1PourLHoriMov[3,]), mean(SOA1PourLHoriMov[4,]),
                           mean(SOA1PourLHoriMov[5,]), mean(SOA1PourLHoriMov[6,]), mean(SOA1PourLHoriMov[7,]), mean(SOA1PourLHoriMov[8,]),
                           mean(SOA1PourLHoriMov[9,]), mean(SOA1PourLHoriMov[10,]), mean(SOA1PourLHoriMov[11,])), 
                         c(sd(SOA1PourLHoriMov[1,]), sd(SOA1PourLHoriMov[2,]), sd(SOA1PourLHoriMov[3,]), sd(SOA1PourLHoriMov[4,]),
                           sd(SOA1PourLHoriMov[5,]), sd(SOA1PourLHoriMov[6,]), sd(SOA1PourLHoriMov[7,]), sd(SOA1PourLHoriMov[8,]),
                           sd(SOA1PourLHoriMov[9,]), sd(SOA1PourLHoriMov[10,]), sd(SOA1PourLHoriMov[11,])))
colnames(SOA1PourLResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")

SOA2PourLResult <- cbind(c(mean(SOA2PourL[1,]), mean(SOA2PourL[2,]), mean(SOA2PourL[3,]), mean(SOA2PourL[4,]),
                           mean(SOA2PourL[5,]), mean(SOA2PourL[6,]), mean(SOA2PourL[7,]), mean(SOA2PourL[8,]),
                           mean(SOA2PourL[9,]), mean(SOA2PourL[10,]), mean(SOA2PourL[11,])), 
                         c(sd(SOA2PourL[1,]), sd(SOA2PourL[2,]), sd(SOA2PourL[3,]), sd(SOA2PourL[4,]), 
                           sd(SOA2PourL[5,]), sd(SOA2PourL[6,]),sd(SOA2PourL[7,]), sd(SOA2PourL[8,]), 
                           sd(SOA2PourL[9,]), sd(SOA2PourL[10,]), sd(SOA2PourL[11,])), 
                         c(mean(SOA2PourLHoriMov[1,]), mean(SOA2PourLHoriMov[2,]), mean(SOA2PourLHoriMov[3,]), mean(SOA2PourLHoriMov[4,]),
                           mean(SOA2PourLHoriMov[5,]), mean(SOA2PourLHoriMov[6,]), mean(SOA2PourLHoriMov[7,]), mean(SOA2PourLHoriMov[8,]),
                           mean(SOA2PourLHoriMov[9,]), mean(SOA2PourLHoriMov[10,]), mean(SOA2PourLHoriMov[11,])), 
                         c(sd(SOA2PourLHoriMov[1,]), sd(SOA2PourLHoriMov[2,]), sd(SOA2PourLHoriMov[3,]), sd(SOA2PourLHoriMov[4,]),
                           sd(SOA2PourLHoriMov[5,]), sd(SOA2PourLHoriMov[6,]), sd(SOA2PourLHoriMov[7,]), sd(SOA2PourLHoriMov[8,]),
                           sd(SOA2PourLHoriMov[9,]), sd(SOA2PourLHoriMov[10,]), sd(SOA2PourLHoriMov[11,])))
colnames(SOA2PourLResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")

SOA3PourLResult <- cbind(c(mean(SOA3PourL[1,]), mean(SOA3PourL[2,]), mean(SOA3PourL[3,]), mean(SOA3PourL[4,]),
                           mean(SOA3PourL[5,]), mean(SOA3PourL[6,]), mean(SOA3PourL[7,]), mean(SOA3PourL[8,]),
                           mean(SOA3PourL[9,]), mean(SOA3PourL[10,]), mean(SOA3PourL[11,])), 
                         c(sd(SOA3PourL[1,]), sd(SOA3PourL[2,]), sd(SOA3PourL[3,]), sd(SOA3PourL[4,]), 
                           sd(SOA3PourL[5,]), sd(SOA3PourL[6,]), sd(SOA3PourL[7,]), sd(SOA3PourL[8,]), 
                           sd(SOA3PourL[9,]), sd(SOA3PourL[10,]), sd(SOA3PourL[11,])), 
                         c(mean(SOA3PourLHoriMov[1,]), mean(SOA3PourLHoriMov[2,]), mean(SOA3PourLHoriMov[3,]), mean(SOA3PourLHoriMov[4,]),
                           mean(SOA3PourLHoriMov[5,]), mean(SOA3PourLHoriMov[6,]), mean(SOA3PourLHoriMov[7,]), mean(SOA3PourLHoriMov[8,]),
                           mean(SOA3PourLHoriMov[9,]), mean(SOA3PourLHoriMov[10,]), mean(SOA3PourLHoriMov[11,])), 
                         c(sd(SOA3PourLHoriMov[1,]), sd(SOA3PourLHoriMov[2,]), sd(SOA3PourLHoriMov[3,]), sd(SOA3PourLHoriMov[4,]),
                           sd(SOA3PourLHoriMov[5,]), sd(SOA3PourLHoriMov[6,]), sd(SOA3PourLHoriMov[7,]), sd(SOA3PourLHoriMov[8,]),
                           sd(SOA3PourLHoriMov[9,]), sd(SOA3PourLHoriMov[10,]), sd(SOA3PourLHoriMov[11,])))
colnames(SOA3PourLResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")

SOA4PourLResult <- cbind(c(mean(SOA4PourL[1,]), mean(SOA4PourL[2,]), mean(SOA4PourL[3,]), mean(SOA4PourL[4,]),
                           mean(SOA4PourL[5,]), mean(SOA4PourL[6,]), mean(SOA4PourL[7,]), mean(SOA4PourL[8,]),
                           mean(SOA4PourL[9,]), mean(SOA4PourL[10,]), mean(SOA4PourL[11,])), 
                         c(sd(SOA4PourL[1,]), sd(SOA4PourL[2,]), sd(SOA4PourL[3,]), sd(SOA4PourL[4,]), 
                           sd(SOA4PourL[5,]), sd(SOA4PourL[6,]), sd(SOA4PourL[7,]), sd(SOA4PourL[8,]), 
                           sd(SOA4PourL[9,]), sd(SOA4PourL[10,]), sd(SOA4PourL[11,])), 
                         c(mean(SOA4PourLHoriMov[1,]), mean(SOA4PourLHoriMov[2,]), mean(SOA4PourLHoriMov[3,]), mean(SOA4PourLHoriMov[4,]),
                           mean(SOA4PourLHoriMov[5,]), mean(SOA4PourLHoriMov[6,]), mean(SOA4PourLHoriMov[7,]), mean(SOA4PourLHoriMov[8,]),
                           mean(SOA4PourLHoriMov[9,]), mean(SOA4PourLHoriMov[10,]), mean(SOA4PourLHoriMov[11,])), 
                         c(sd(SOA4PourLHoriMov[1,]), sd(SOA4PourLHoriMov[2,]), sd(SOA4PourLHoriMov[3,]), sd(SOA4PourLHoriMov[4,]),
                           sd(SOA4PourLHoriMov[5,]), sd(SOA4PourLHoriMov[6,]), sd(SOA4PourLHoriMov[7,]), sd(SOA4PourLHoriMov[8,]),
                           sd(SOA4PourLHoriMov[9,]), sd(SOA4PourLHoriMov[10,]), sd(SOA4PourLHoriMov[11,])))
colnames(SOA4PourLResult) <- c("MeanPeakVelocs", "SDPeakVelos", "MeanPosPalmX", "SDPosPalmX")




#########Mean PeakVelos
plot(seq(1,11, by=1),SOA1PlaceLResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")
plot(seq(1,11, by=1),SOA2PlaceLResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")
plot(seq(1,11, by=1),SOA3PlaceLResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")
plot(seq(1,11, by=1),SOA4PlaceLResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")

plot(seq(1,11, by=1),SOA1PlaceRResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")
plot(seq(1,11, by=1),SOA2PlaceRResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")
plot(seq(1,11, by=1),SOA3PlaceRResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")
plot(seq(1,11, by=1),SOA4PlaceRResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")

plot(seq(1,11, by=1),SOA1PourRResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")
plot(seq(1,11, by=1),SOA2PourRResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")
plot(seq(1,11, by=1),SOA3PourRResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")
plot(seq(1,11, by=1),SOA4PourRResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")

plot(seq(1,11, by=1),SOA1PourLResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")
plot(seq(1,11, by=1),SOA2PourLResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")
plot(seq(1,11, by=1),SOA3PourLResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")
plot(seq(1,11, by=1),SOA4PourLResult[,1], xlab="Reaching Distance %", main = "Peak Velocity in Z depth")
#########


#########SD PalmX Pos
plot(seq(1,11, by=1),SOA1PlaceLResult[,4], xlab="Reaching Distance %", main = "SD Palm X")
plot(seq(1,11, by=1),SOA2PlaceLResult[,4], xlab="Reaching Distance %", main = "SD Palm X")
plot(seq(1,11, by=1),SOA3PlaceLResult[,4], xlab="Reaching Distance %", main = "SD Palm X")
plot(seq(1,11, by=1),SOA4PlaceLResult[,4], xlab="Reaching Distance %", main = "SD Palm X")

plot(seq(1,11, by=1),SOA1PlaceRResult[,4], xlab="Reaching Distance %", main = "SD Palm X")
plot(seq(1,11, by=1),SOA2PlaceRResult[,4], xlab="Reaching Distance %", main = "SD Palm X")
plot(seq(1,11, by=1),SOA3PlaceRResult[,4], xlab="Reaching Distance %", main = "SD Palm X")
plot(seq(1,11, by=1),SOA4PlaceRResult[,4], xlab="Reaching Distance %", main = "SD Palm X")

plot(seq(1,11, by=1),SOA1PourRResult[,4], xlab="Reaching Distance %", main = "SD Palm X")
plot(seq(1,11, by=1),SOA2PourRResult[,4], xlab="Reaching Distance %", main = "SD Palm X")
plot(seq(1,11, by=1),SOA3PourRResult[,4], xlab="Reaching Distance %", main = "SD Palm X")
plot(seq(1,11, by=1),SOA4PourRResult[,4], xlab="Reaching Distance %", main = "SD Palm X")

plot(seq(1,11, by=1),SOA1PourLResult[,4], xlab="Reaching Distance %", main = "SD Palm X")
plot(seq(1,11, by=1),SOA2PourLResult[,4], xlab="Reaching Distance %", main = "SD Palm X")
plot(seq(1,11, by=1),SOA3PourLResult[,4], xlab="Reaching Distance %", main = "SD Palm X")
plot(seq(1,11, by=1),SOA4PourLResult[,4], xlab="Reaching Distance %", main = "SD Palm X")
#########




SOA1ResultPalmX <- cbind(SOA1PlaceLResult[,3], SOA1PlaceRResult[,3], SOA1PourLResult[,3], SOA1PourRResult[,3])
sd1 <- c(sd(SOA1ResultPalmX[1,]),sd(SOA1ResultPalmX[2,]),sd(SOA1ResultPalmX[3,]),sd(SOA1ResultPalmX[4,]),
          sd(SOA1ResultPalmX[5,]),sd(SOA1ResultPalmX[6,]),sd(SOA1ResultPalmX[7,]),sd(SOA1ResultPalmX[8,]),
          sd(SOA1ResultPalmX[9,]),sd(SOA1ResultPalmX[10,]),sd(SOA1ResultPalmX[11,]))

SOA2ResultPalmX <- cbind(SOA2PlaceLResult[,3], SOA2PlaceRResult[,3], SOA2PourLResult[,3], SOA2PourRResult[,3])
sd2 <- c(sd(SOA2ResultPalmX[1,]),sd(SOA2ResultPalmX[2,]),sd(SOA2ResultPalmX[3,]),sd(SOA2ResultPalmX[4,]),
          sd(SOA2ResultPalmX[5,]),sd(SOA2ResultPalmX[6,]),sd(SOA2ResultPalmX[7,]),sd(SOA2ResultPalmX[8,]),
          sd(SOA2ResultPalmX[9,]),sd(SOA2ResultPalmX[10,]),sd(SOA2ResultPalmX[11,]))

SOA3ResultPalmX <- cbind(SOA3PlaceLResult[,3], SOA3PlaceRResult[,3], SOA3PourLResult[,3], SOA3PourRResult[,3])
sd3 <- c(sd(SOA3ResultPalmX[1,]),sd(SOA3ResultPalmX[2,]),sd(SOA3ResultPalmX[3,]),sd(SOA3ResultPalmX[4,]),
         sd(SOA3ResultPalmX[5,]),sd(SOA3ResultPalmX[6,]),sd(SOA3ResultPalmX[7,]),sd(SOA3ResultPalmX[8,]),
         sd(SOA3ResultPalmX[9,]),sd(SOA3ResultPalmX[10,]),sd(SOA3ResultPalmX[11,]))

SOA4ResultPalmX <- cbind(SOA4PlaceLResult[,3], SOA4PlaceRResult[,3], SOA4PourLResult[,3], SOA4PourRResult[,3])
sd4 <- c(sd(SOA4ResultPalmX[1,]),sd(SOA4ResultPalmX[2,]),sd(SOA4ResultPalmX[3,]),sd(SOA4ResultPalmX[4,]),
         sd(SOA4ResultPalmX[5,]),sd(SOA4ResultPalmX[6,]),sd(SOA4ResultPalmX[7,]),sd(SOA4ResultPalmX[8,]),
         sd(SOA4ResultPalmX[9,]),sd(SOA4ResultPalmX[10,]),sd(SOA4ResultPalmX[11,]))


plot(seq(1,11, by=1),sd1, xlab="Reaching Distance %", main = "SD Palm X all SOA1")
plot(seq(1,11, by=1),sd2, xlab="Reaching Distance %", main = "SD Palm X all SOA2")
plot(seq(1,11, by=1),sd3, xlab="Reaching Distance %", main = "SD Palm X all SOA3")
plot(seq(1,11, by=1),sd4, xlab="Reaching Distance %", main = "SD Palm X all SOA4")
















###################testing


# quantileTrialList <- c()
# for (i in seq(0,1,0.1)) {
#   #print(quantile(DataAllReachForBottleTrials$PalmZ,i))
#   quantileTrialList <- c(quantileTrialList, unname(quantile(DataAllReachForBottleTrials$PalmZ,i)))
# }
# rowCounter <- 1
# trialVelos <- list()
# for (i in 1:length(quantileTrialList)) {
#   for (j in 1:length(DataAllReachForBottleTrials$PalmZVelo)){
#     higherBorder <- quantileTrialList[i] * 1.03
#     lowerBorder <- quantileTrialList[i] * 0.97
#     #print(paste(DataAllReachForBottleTrials$PalmZ[i], higherBorder, lowerBorder))
#     if(between(DataAllReachForBottleTrials$PalmZ[j], higherBorder, lowerBorder)){
#       rowIndice <- which(round(DataAllReachForBottleTrials$PalmZ, digits = 5) == round(DataAllReachForBottleTrials$PalmZ[i],digits = 5))
#       print(rowIndice)
#       print(DataAllReachForBottleTrials$PalmZVelo[rowIndice])
#       #print(paste("j and counter: ",j,counter1))
#       trialVelos <- c(trialVelos, DataAllReachForBottleTrials$PalmZVelo[rowIndice])
#       rowCounter <- rowCounter + 1
#     }
#   }
# }
# counter1 <- counter1 + 1

#######
####### Dat is es, also einfach an die Liste dranhängen und dann True filtern
#######
# matchedVelos <- c()
# counter1 <- 1
# # for (i in 1:length(quantileTrialList)){
# #   higherBorder <- quantileTrialList[i] * 1.03
# #   lowerBorder <- quantileTrialList[i] * 0.97
# #   DataAllReachForBottleTrials$Truez <- between(DataAllReachForBottleTrials$PalmZ, higherBorder, lowerBorder)
# #   trueIndex <- (1:nrow(DataAllReachForBottleTrials))[DataAllReachForBottleTrials[,24] == "TRUE"]
# #   matchedVelos <- c(matchedVelos, DataAllReachForBottleTrials$PalmZVelo[trueIndex[1]])
# # }
# PeakVelocitySOA1PlaceLeft[,counter1] <- c(matchedVelos)
# counter1 <- counter1 + 1





# which(round(DataAllReachForBottleTrials$PalmZ, digits = 5) == -30.04609)
# 
# match(DataAllReachForBottleTrials$PalmZ,"-30.0460862976618")
# 
# counter1 <- 1
# 
# PeakVelocitySOA1PlaceLeft[,counter1] <- quantileTrialList 
# counter1 <- counter1 + 1



# percentageDistance <- seq(-30.4,-17.5,length.out = 10)
# quantile(DataAllReachForBottleTrials$PalmZ,0.1)



#############################################
#############################################
#testing stuff
# dataMA <- read.table("AnnotatedData/01/20_01_SOA1_Place_Left.log", header = TRUE)
# 
# thumbXYZ <-  str_split_fixed(dataMA$ThumbPosition, ":", 3)
# indexXYZ <-  str_split_fixed(dataMA$IndexPosition, ":", 3)
# palmXYZ <-  str_split_fixed(dataMA$PalmPosition, ":", 3)
# objectXYZ <-  str_split_fixed(dataMA$ObjectPosition, ":", 3)
# gazeXYZ <-  str_split_fixed(dataMA$GazePosition, ":", 3)


# dataMA$ThumbX <- as.numeric(thumbXYZ[,1])  
# dataMA$ThumbY <- as.numeric(thumbXYZ[,2])
# dataMA$ThumbZ <- as.numeric(thumbXYZ[,3])
# 
# dataMA$IndexX <- as.numeric(indexXYZ[,1])  
# dataMA$IndexY <- as.numeric(indexXYZ[,2])
# dataMA$IndexZ <- as.numeric(indexXYZ[,3])
# 
# dataMA$PalmX <- as.numeric(palmXYZ[,1])  
# dataMA$PalmY <- as.numeric(palmXYZ[,2])
# dataMA$PalmZ <- as.numeric(palmXYZ[,3])
# 
# dataMA$ObjectX <- as.numeric(objectXYZ[,1])  
# dataMA$ObjectY <- as.numeric(objectXYZ[,2])
# dataMA$ObjectZ <- as.numeric(objectXYZ[,3])
# 
# dataMA$GazeX <- as.numeric(gazeXYZ[,1])  
# dataMA$GazeY <- as.numeric(gazeXYZ[,2])
# dataMA$GazeZ <- as.numeric(gazeXYZ[,3])
# 
# dataMA <- na.omit(dataMA)

# allReachForBottleRowNumbers <- (1:nrow(dataMA))[dataMA[,6] == "ReachForBottle"]
# firstRowIndexWithReachForBottle <- allReachForBottleRowNumbers[1]
# lastRowIndexWithReachForBottle <- allReachForBottleRowNumbers[length(allReachForBottleRowNumbers)]
# DataAllReachForBottleTrials <- dataMA[(firstRowIndexWithReachForBottle-5):(lastRowIndexWithReachForBottle+5),]
# 
# DataAllReachForBottleTrials <- na.omit(DataAllReachForBottleTrials)
# 
# 
# 
# 
# 
# 
# t <- seq(1, length(DataAllReachForBottleTrials$PalmZ), len = length(DataAllReachForBottleTrials$PalmZ))
# derivations <- finiteDifferences(t, DataAllReachForBottleTrials$PalmZ)
# DataAllReachForBottleTrials$PalmZVelo <- derivations
# plot(t,DataAllReachForBottleTrials$PalmZVelo, main = filename)







##################Butterworth
#filtering, butterworth 8-12hz second order
# butterworthFilter <- butter(2, 0.10)
# 
# x <- dataMA$ThumbZ
# dataMA$ThumbX <- filter(butterworthFilter, dataMA$ThumbX)
# dataMA$ThumbY <- filter(butterworthFilter, dataMA$ThumbY)
# dataMA$ThumbZ <- filter(butterworthFilter, dataMA$ThumbZ)
# 
# dataMA$IndexX <- filter(butterworthFilter, dataMA$IndexX)
# dataMA$IndexY <- filter(butterworthFilter, dataMA$IndexY)
# dataMA$IndexZ <- filter(butterworthFilter, dataMA$IndexZ)
# 
# dataMA$PalmX <- filter(butterworthFilter, dataMA$PalmX)
# dataMA$PalmY <- filter(butterworthFilter, dataMA$PalmY)
# dataMA$PalmZ <- filter(butterworthFilter, dataMA$PalmZ)
# 
# dataMA$ObjectX <- filter(butterworthFilter, dataMA$ObjectX)
# dataMA$ObjectY <- filter(butterworthFilter, dataMA$ObjectY)
# dataMA$ObjectZ <- filter(butterworthFilter, dataMA$ObjectZ)
# 
# dataMA$GazeX <- filter(butterworthFilter, dataMA$GazeX)
# dataMA$GazeY <- filter(butterworthFilter, dataMA$GazeY)
# dataMA$GazeZ <- filter(butterworthFilter, dataMA$GazeZ)

#testMatrixX <- cbind(dataMA$ThumbPosition,dataMA$ThumbX)
#testMatrixZ <- cbind(dataMA$ThumbPosition,dataMA$ThumbZ)


# z <- filter(butterworthFilter, x)
# t <- seq(0, 197, len = 197)
# plot(t, x, type='l', main = "ThumbZ, filter, fc 0.10")
# #lines(t, y, col="red")
# lines(t,z,col="blue")
##################Butterworth
